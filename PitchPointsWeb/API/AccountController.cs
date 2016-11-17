using PitchPointsWeb.Models;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using static PitchPointsWeb.API.APICommon;
using static PitchPointsWeb.API.AccountVerifier;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace PitchPointsWeb.API
{
    public class AccountController : ApiController
    {

        /// <summary>
        /// Attempts to register a new user in the database. A successful dictionary will contain PrivateKeyInfo. An unsuccessful dictionary will contain an error with a user friendly message as to why they couldn't register.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] RegisterAPIUser user)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
            }
            catch
            {
                return GetUnavailableMessage();
            }
            var dict = new Dictionary<string, object>();
            if (DoesUserExist(user.Email))
            {
                dict.Add("Error", "User already exists");
            } else
            {
                var insertResponse = InsertUser(user);
                if (!insertResponse.Success)
                {
                    dict.Add("Error", insertResponse.ErrorMessage);
                } else
                {
                    dict.Add("PrivateKeyInfo", insertResponse.PrivateKeyInfo);
                }
            }
            connection.Close();
            dict.Add("Success", !dict.ContainsKey("Error"));
            return CreateJsonResponse(dict);
        }

        /// <summary>
        /// Attempts to 'login' to the website by using a LoginAPIUser model. A successful dictionary would contain PrivateKeyInfo. An unsuccessful dictionary would include an error pertaining to why a login was invalid.
        /// </summary>
        /// <param name="user">The user to login with</param>
        /// <returns>A JSON dictionary with a private key if the login was successful</returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginAPIUser user)
        {
            var dict = new Dictionary<string, object>();
            if (DoesUserExist(user.Email))
            {
                var connection = GetConnection();
                connection.Open();
                var dbUser = GetUserFrom(user.Email);
                if (!dbUser.ID.HasValue)
                {
                    dict["Error"] = "User does not exist with this email";
                } else if (dbUser.PasswordsMatch(user.Password))
                {
                    var userId = dbUser.ID.Value;
                    dict.Add("PrivateKeyInfo", CreateAndInsertPublicKeyFrom(userId, connection));
                } else
                {
                    dict["Error"] = "Incorrect password";
                }
                connection.Close();
            } else
            {
                dict["Error"] = "User does not exist with this email";
            }
            dict["Success"] = !dict.ContainsKey("Error");
            return CreateJsonResponse(dict);
        }

        /// <summary>
        /// Creates a user object from the database with the given email
        /// </summary>
        /// <param name="email">The email to use to query the database</param>
        /// <returns>A User if the email exists within the database, null otherwise</returns>
        private User GetUserFrom(string email)
        {
            var connection = GetConnection();
            connection.Open();
            User user = null;
            using (var command = new SqlCommand("GetAllUserInfo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@email", email);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = ReadUserFrom(reader);
                }
                reader.Close();
            }
            connection.Close();
            return user;
        }

        /// <summary>
        /// Determines if a user exists in the database based on their email
        /// </summary>
        /// <param name="email">the email to check within the database</param>
        /// <returns>True if the user exists in the database</returns>
        /// <exception cref="SqlException">Thrown if there is an issue creating a connection to the database</exception> 
        private bool DoesUserExist(string email)
        {
            return GetUserFrom(email) != null;
        }

        private InsertUserResult InsertUser(RegisterAPIUser registerUser)
        {
            Debug.WriteLine(registerUser.DateOfBirth);
            var user = Models.User.CreateFrom(registerUser);
            var connection = GetConnection();
            connection.Open();
            var success = false;
            var message = "";
            var userId = 0;
            PrivateKeyInfoResult info = new PrivateKeyInfoResult();
            using (var command = new SqlCommand("CreateUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                var userIdParam = new SqlParameter("@UserId", SqlDbType.Int);
                userIdParam.Direction = ParameterDirection.Output;
                var errorCode = new SqlParameter("@ErrorCode", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;
                var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 64);
                errorMessage.Direction = ParameterDirection.Output;
                command.Parameters.Add(userIdParam);
                command.Parameters.Add(errorCode);
                command.Parameters.Add(errorMessage);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@dob", user.DateOfBirth);
                command.Parameters.AddWithValue("@password", user.PasswordHash);
                command.Parameters.AddWithValue("@salt", user.Salt);
                var reader = command.ExecuteReader();
                success = (errorCode.Value as int?) == 0;
                message = errorMessage.Value as string ?? "";
                userId = (int) userIdParam.Value;
                reader.Close();
                if (success)
                {
                    info = CreateAndInsertPublicKeyFrom(userId, connection);
                }
            }
            connection.Close();
            return new InsertUserResult()
            {
                Success = success,
                ErrorMessage = message,
                UserId = userId,
                PrivateKeyInfo = info
            };
        }

        private PrivateKeyInfoResult CreateAndInsertPublicKeyFrom(int userID, SqlConnection connection = null)
        {
            var pair = GenerateKeyPair();
            connection = connection ?? GetConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            var id = 0;
            using (var command = new SqlCommand("InsertPublicKey", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new SqlParameter("@id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParam);
                command.Parameters.AddWithValue("@userId", userID);
                command.Parameters.AddWithValue("@key", pair.Item1);
                var reader = command.ExecuteReader();
                id = (idParam.Value as int?) ?? 0;
                reader.Close();
            }
            connection.Close();
            return new PrivateKeyInfoResult()
            {
                PrivateKey = pair.Item2,
                PublicKeyId = id
            };
        }

        /// <summary>
        /// Attempts to extract a user from a SqlDataReader
        /// </summary>
        /// <param name="reader">A SqlDataReader obtained from a SqlCommand execution</param>
        /// <returns>A user with all data provided in the reader</returns>
        private User ReadUserFrom(SqlDataReader reader)
        {
            var user = new User();
            user.ID = readObject(reader, "UserId", 0);
            user.FirstName = readObject(reader, "FirstName", "");
            user.LastName = readObject(reader, "LastName", "");
            var dob = readObjectOrNull<DateTime>(reader, "DOB");
            if (dob.HasValue)
            {
                user.DateOfBirth = dob.Value;
            }
            user.Email = readObject(reader, "Email", "");
            user.EmailVerified = readObject(reader, "EmailConfirmed", (byte) 0) == (byte) 1;
            user.PasswordHash = readObject<byte[]>(reader, "PasswordHash", null);
            user.Salt = readObject<byte[]>(reader, "Salt", null);
            var dateRegistered = readObjectOrNull<DateTime>(reader, "DateRegistered");
            if (dateRegistered.HasValue)
            {
                user.DateRegistered = dateRegistered.Value;
            }
            return user;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        private struct InsertUserResult
        {

            public bool Success;

            public string ErrorMessage;

            public int UserId;

            public PrivateKeyInfoResult PrivateKeyInfo;

        }

        private struct PrivateKeyInfoResult
        {

            public byte[] PrivateKey;

            public int PublicKeyId;

        }

    }
}
