using PitchPointsWeb.Models;
using System.Web.Http;
using System;
using System.Net.Http;
using static PitchPointsWeb.API.AccountVerifier;
using System.Data.SqlClient;
using System.Data;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.Models.API.Response;
using System.Collections.Generic;

namespace PitchPointsWeb.API
{
    public class AccountController : MasterController
    {

        /// <summary>
        /// Attempts to register a user from a RegisterAPIUser model and returns a HttpResponseMessage
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns>A HttpResponseMessage based on the success / failure of the insertion</returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] RegisterModel user)
        {
            return CreateJsonResponse(InternalRegister(user));
        }

        /// <summary>
        /// Internally registers a user in the database.
        /// </summary>
        /// <param name="user">The user to register in the database</param>
        /// <returns>A RegisterAccountResponse from the insertion. Use this value to check if the insertion was successful and for any potential errors.</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
        internal RegisterAccountResponse InternalRegister(RegisterModel user)
        {
            RegisterAccountResponse response = new RegisterAccountResponse();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    if (DoesUserExist(user.Email))
                    {
                        response.APIResponseCode = APIResponseCode.USER_ALREADY_EXISTS_EMAIL;
                    }
                    else
                    {
                        var insertResponse = InsertUser(user, connection);
                        response.ResponseCode = insertResponse.ResponseCode;
                        if (insertResponse.Success)
                        {
                            response.PrivateKey = insertResponse.PrivateKeyInfo;
                        }
                    }
                }
            } catch
            {
                response.APIResponseCode = APIResponseCode.INTERNAL_ERROR;
            }
            return response;
        }

        /// <summary>
        /// Attempts to 'login' to the website by using a LoginAPIUser model. A successful dictionary would contain
        /// PrivateKeyInfo. An unsuccessful dictionary would include an error pertaining to why a login was invalid.
        /// </summary>
        /// <param name="user">The user to login with</param>
        /// <returns>A JSON dictionary with a private key if the login was successful</returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginModel user)
        {
            return CreateJsonResponse(InternalLogin(user));
        }

        /// <summary>
        /// Attempts to validate a user trying to log in
        /// </summary>
        /// <param name="user">The user attempting to log in</param>
        /// <returns>A LoginAccountResponse which dictates if the login was successful or not</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
        internal LoginAccountResponse InternalLogin(LoginModel user)
        {
            LoginAccountResponse response = new LoginAccountResponse();
            try
            {
                var databaseUser = GetUserFrom(user.Email);
                if (databaseUser == null)
                {
                    response.APIResponseCode = APIResponseCode.USER_DOES_NOT_EXIST_EMAIL;
                    return response;
                }
                var connection = GetConnection();
                connection.Open();
                if (databaseUser.PasswordsMatch(user.Password))
                {
                    response.APIResponseCode = APIResponseCode.SUCCESS;
                    response.PrivateKey = CreateAndInsertPublicKeyFrom(databaseUser.ID.Value, connection);
                }
                else
                {
                    response.APIResponseCode = APIResponseCode.INCORRECT_PASSWORD;
                }
                connection.Close();
            } catch
            {
                response.APIResponseCode = APIResponseCode.INTERNAL_ERROR;
            }
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
            try
            {
                using (var connection = GetConnection())
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
                }
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
            try
            {
                using (var connection = GetConnection())
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
                }
            } catch
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public UserSnapshot GetUserSnapshot([FromBody] UserIDSignedData data)
        {
            UserSnapshot snapshot = new UserSnapshot();
            if (!data.IsValid())
            {
                snapshot.APIResponseCode = APIResponseCode.AUTH_ERROR;
                return snapshot;
            }
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetUserSnapshot", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userId", data.UserID);
                        using (var reader = command.ExecuteReader())
                        {
                            snapshot.Points = readObject(reader, "Points", 0);
                            snapshot.Falls = readObject(reader, "Falls", 0);
                            snapshot.ParticipatedCompetitions = readObject(reader, "ParticipatedCompetitions", 0);
                            if (reader.NextResult())
                            {
                                List<Competition> competitions = new List<Competition>();
                                while (reader.Read())
                                {
                                    competitions.Add(readCompetition(reader));
                                }
                                snapshot.UpcomingCompetitions = competitions;
                            }
                        }
                    }
                }
            }
            catch
            {
                snapshot.APIResponseCode = APIResponseCode.INTERNAL_ERROR;
            }
            return snapshot;
        }

        /// <summary>
        /// Creates a user object from the database with the given email
        /// </summary>
        /// <param name="email">The email to use to query the database</param>
        /// <returns>A User if the email exists within the database, null otherwise</returns>
        /// <exception cref="SqlException">Throws if there is an error connecting to the database or any SQL related exception or warning thrown</exception>
        private User GetUserFrom(string email)
        {
            User user = null;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand("GetAllUserInfo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            user = ReadUserFrom(reader);
                        }
                    }
                }
            }
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

        private InsertUserResponse InsertUser(RegisterModel model, SqlConnection connection)
        {
            var user = Models.User.CreateFrom(model);
            var response = new InsertUserResponse();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (var command = new SqlCommand("CreateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var userIdParam = new SqlParameter("@UserId", SqlDbType.Int);
                    userIdParam.Direction = ParameterDirection.Output;
                    var errorCode = new SqlParameter("@ResponseCode", SqlDbType.Int);
                    errorCode.Direction = ParameterDirection.Output;
                    var errorMessage = new SqlParameter("@ResponseMessage", SqlDbType.VarChar, 64);
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
                    using (var reader = command.ExecuteReader())
                    {
                        var outErrorCode = (errorCode.Value as int?) ?? 0;
                        var userId = (int)userIdParam.Value;
                        if (outErrorCode == 0)
                        {
                            response.APIResponseCode = APIResponseCode.SUCCESS;
                            response.UserId = userId;
                            response.PrivateKeyInfo = CreateAndInsertPublicKeyFrom(userId, connection);
                        }
                        else
                        {
                            response.APIResponseCode = (APIResponseCode)outErrorCode;
                        }
                    }
                }
            } catch
            {
                response.APIResponseCode = APIResponseCode.INTERNAL_ERROR;
            }
            return response;
        }

        /// <summary>
        /// Creates and inserts a Public Key / Public Key ID into the database with a given connection.
        /// The connection provided is not closed and must be closed by the invoked methods used to
        /// call this method.
        /// </summary>
        /// <param name="userID">The user ID to create a key pair for</param>
        /// <param name="connection">The pre-existing connection to use to access the database</param>
        /// <returns>A PrivateKeyInfo for the user</returns>
        private PrivateKeyInfo CreateAndInsertPublicKeyFrom(int userID, SqlConnection connection)
        {
            var pair = GenerateKeyPair();
            var id = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            using (var command = new SqlCommand("InsertPublicKey", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                var idParam = new SqlParameter("@id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParam);
                command.Parameters.AddWithValue("@userId", userID);
                command.Parameters.AddWithValue("@key", pair.Item1);
                using (var reader = command.ExecuteReader())
                {
                    id = (idParam.Value as int?) ?? 0;
                }
            }
            return new PrivateKeyInfo()
            {
                PrivateKey = Convert.ToBase64String(pair.Item2, options: Base64FormattingOptions.None),
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

    }
}
