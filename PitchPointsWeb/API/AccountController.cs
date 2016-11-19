using PitchPointsWeb.Models;
using System.Web.Http;
using System;
using System.Net.Http;
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
        /// Attempts to register a user from a RegisterAPIUser model and returns a HttpResponseMessage
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>A HttpResponseMessage based on the success / failure of the insertion</returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] RegisterAPIUser user)
        {
            RegisterAccountResponse response = null;
            try
            {
                response = InternalRegister(user);
            } catch (SqlException)
            {
                return GetUnavailableMessage();
            }
            return CreateJsonResponse(response);
        }

        /// <summary>
        /// Internally registers a user in the database.
        /// </summary>
        /// <param name="user">The user to register in the database</param>
        /// <returns>A RegisterAccountResponse from the insertion. Use this value to check if the insertion was successful and for any potential errors.</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
        internal RegisterAccountResponse InternalRegister(RegisterAPIUser user)
        {
            var connection = GetConnection();
            connection.Open();
            var response = new RegisterAccountResponse();
            if (DoesUserExist(user.Email))
            {
                response.ErrorMessage = "Email unavailable";
            } else
            {
                var insertResponse = InsertUser(user);
                if (insertResponse.Success)
                {
                    response.PrivateKey = insertResponse.PrivateKeyInfo;
                } else
                {
                    response.ErrorMessage = insertResponse.ErrorMessage;
                }
            }
            response.Success = response.ErrorMessage == null;
            connection.Close();
            return response;
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
            LoginAccountResponse response = null;
            try
            {
                response = InternalLogin(user);
            } catch
            {
                return GetUnavailableMessage();
            }
            return CreateJsonResponse(response);
        }

        /// <summary>
        /// Attempts to validate a user trying to log in
        /// </summary>
        /// <param name="user">The user attempting to log in</param>
        /// <returns>A LoginAccountResponse which dictates if the login was successful or not</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
        internal LoginAccountResponse InternalLogin(LoginAPIUser user)
        {
            LoginAccountResponse response = new LoginAccountResponse();
            var databaseUser = GetUserFrom(user.Email);
            if (databaseUser != null)
            {
                var connection = GetConnection();
                connection.Open();
                if (databaseUser.PasswordsMatch(user.Password))
                {
                    response.PrivateKey = CreateAndInsertPublicKeyFrom(databaseUser.ID.Value, connection);
                } else
                {
                    response.ErrorMessage = "Incorrect password";
                }
                connection.Close();
            } else
            {
                response.ErrorMessage = "User does not exist with this email";
            }
            response.Success = response.ErrorMessage == null;
            return response;
        }

        /// <summary>
        /// Modifies a user based on their ID to grant / revoke admin permission(s)
        /// </summary>
        /// <param name="userId">The ID of a user that exists in the User table</param>
        /// <param name="isAdmin">Determines if this user is an admin. Set this to false to remove this user from the admin table</param>
        /// <param name="canModifyScores">True if this admin can modify climber scores in a competition</param>
        /// <param name="canCreateCompetitions">True if this admin has competition creation privileges</param>
        /// <returns>True if the modification was successful, false otherwise</returns>
        internal bool ModifyAdmin(int userId, bool isAdmin, bool canModifyScores, bool canCreateCompetitions)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
                using (var command = new SqlCommand("ModifyAdminUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);
                    command.Parameters.AddWithValue("@canModifyScores", canModifyScores);
                    command.Parameters.AddWithValue("@canCreateCompetitions", canCreateCompetitions);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            } catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Modifies a user based on their ID to promote / revoke judge role
        /// </summary>
        /// <param name="userId">The ID of a user that exists in the User table</param>
        /// <param name="isJudge">Determines if this user is a judge. Set this to false to remove this user from the judge table</param>
        /// <param name="compId">The ID of a competition that this user can be a judge for</param>
        /// <returns>True if the modification was successful, false otherwise</returns>
        internal bool ModifyJudge(int userId, bool isJudge, int compId)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
                using (var command = new SqlCommand("ModifyJudgeUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@isJudge", isJudge);
                    command.Parameters.AddWithValue("@compId", compId);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            } catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates a user object from the database with the given email
        /// </summary>
        /// <param name="email">The email to use to query the database</param>
        /// <returns>A User if the email exists within the database, null otherwise</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
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
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception> 
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
            var info = new PrivateKeyInfo();
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

        private PrivateKeyInfo CreateAndInsertPublicKeyFrom(int userID, SqlConnection connection = null)
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
            return new PrivateKeyInfo()
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

        private struct InsertUserResult
        {

            public bool Success;

            public string ErrorMessage;

            public int UserId;

            public PrivateKeyInfo PrivateKeyInfo;

        }

    }
}
