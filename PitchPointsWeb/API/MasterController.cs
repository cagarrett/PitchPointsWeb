using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Jose;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using PitchPointsWeb.Models;
using PitchPointsWeb.Models.API;

namespace PitchPointsWeb.API
{
    public class MasterController : ApiController
    {

        private static readonly KeyVaultClient KeyClient = new KeyVaultClient(async (auth, resource, scope) =>
        {
            var credential = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"], WebConfigurationManager.AppSettings["ClientSecret"]);
            var context = new AuthenticationContext(auth, null);
            var token = await context.AcquireTokenAsync(resource, credential);
            return token.AccessToken;
        });

        /// <summary>
        /// Obtains the default connection used in this project
        /// </summary>
        /// <returns>A SqlConnection used throughout the API</returns>
        internal static SqlConnection GetConnection()
        {
            return new SqlConnection
            {
                ConnectionString =
                    System.Configuration.ConfigurationManager.ConnectionStrings["PitchPointsDB"].ConnectionString
            };
        }

        internal async Task<string> CreateToken(User user, bool isAdmin)
        {
            var payload = new Dictionary<string, object>
            {
                {"email", user.Email},
                {"exp", DateTime.Now.AddHours(isAdmin ? 2 : 1).Ticks },
                {"admin", isAdmin }
            };
            var key = await KeyClient.GetSecretAsync(WebConfigurationManager.AppSettings["SecretUri"]);
            var keyBytes = Encoding.UTF8.GetBytes(key.Value);
            string token;
            try
            {
                token = JWT.Encode(payload, keyBytes, JwsAlgorithm.HS256);
            }
            catch
            {
                token = null;
            }
            return token;
        }

        internal async Task<TokenContentModel> ConvertTokenToModel(string token)
        {
            var key = await KeyClient.GetSecretAsync(WebConfigurationManager.AppSettings["SecretUri"]);
            var keyBytes = Encoding.UTF8.GetBytes(key.Value);
            var model = new TokenContentModel();
            try
            {
                var json = JWT.Decode(token, keyBytes, JwsAlgorithm.HS256);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                model.Email = dict["email"];
                model.ExpDateTime = new DateTime((long) dict["exp"]);
            }
            catch
            {
                // ignored
            }
            return model;
        }

        /// <summary>
        /// Creates an ID table that is used within the database when passing more then 1 id is desired
        /// </summary>
        /// <param name="ids">An array of integers to be used as IDS</param>
        /// <returns>A DataTable that has a single column (ID) with the ids passed in as rows</returns>
        internal static DataTable CreateIdTable(int[] ids)
        {
            var idTable = new DataTable();
            idTable.Columns.Add("ID", typeof(int));
            foreach (var id in ids)
            {
                idTable.Rows.Add(id);
            }
            return idTable;
        }

        /// <summary>
        /// Attempts to read an object from a SqlDataReader from the column provided
        /// </summary>
        /// <typeparam name="T">The type of object expected in reader[column]</typeparam>
        /// <param name="reader">The reader to extract the column info from</param>
        /// <param name="column">The desired column</param>
        /// <param name="defaultValue">A default value to fallback on if there is a casting issue</param>
        /// <returns>reader[column] as T or defaultValue otherwise</returns>
        internal static T ReadObject<T>(SqlDataReader reader, string column, T defaultValue)
        {
            try
            {
                return (T)reader[column];
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Attempts to read an object from a SqlDataReader from the column provided and returns null if there is an issue
        /// </summary>
        /// <typeparam name="T">The type of the object expected in reader[column]</typeparam>
        /// <param name="reader">The reader tp extract the column info from</param>
        /// <param name="column">The desired column</param>
        /// <returns>reader[column] as T or null otherwise</returns>
        internal static T? ReadObjectOrNull<T>(SqlDataReader reader, string column) where T : struct
        {
            try
            {
                return (T)reader[column];
            }
            catch
            {
                return null;
            }
        }

    }
}
