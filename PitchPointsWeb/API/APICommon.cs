using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace PitchPointsWeb.API
{

    public class APICommon
    {
        
        /// <summary>
        /// Obtains the default connection used in this project
        /// </summary>
        /// <returns>A SqlConnection used throughout the API</returns>
        internal static SqlConnection GetConnection()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PitchPointsDB"].ConnectionString;
            return connection;
        }

        /// <summary>
        /// Creates a simple BadRequest message
        /// </summary>
        /// <param name="reason">The reason for the bad request</param>
        /// <returns>A HttpResponseMessage with a BadRequest status code and an error with the reason provided</returns>
        internal static HttpResponseMessage GetBadRequestMessage(string reason)
        {
            var dict = new Dictionary<string, object>()
            {
                { "Error", reason }
            };
            return CreateJsonResponse(dict, HttpStatusCode.BadRequest);
        }

        internal static HttpResponseMessage GetBadAuthRequest()
        {
            return CreateJsonResponse("Invalid credentials", HttpStatusCode.Unauthorized);
        }

        /// <summary>
        /// Creates a simple Unavailable message
        /// </summary>
        /// <returns>A HttpResponseMessage with a ServiceUnavailable status code</returns>
        internal static HttpResponseMessage GetUnavailableMessage()
        {
            var dict = new Dictionary<string, object>()
            {
                { "Error", "Web service unavailable" }
            };
            return CreateJsonResponse(dict, HttpStatusCode.ServiceUnavailable);
        }

        /// <summary>
        /// Creates a simple JSON message with a provided Dictionary<string, object>
        /// </summary>
        /// <param name="dictionary">The dictionary to transform to JSON</param>
        /// <param name="status">The status code for this message. Default = OK</param>
        /// <returns>A HttpResponseMessage formatted as JSON with content being the dictionary passed in</returns>
        internal static HttpResponseMessage CreateJsonResponse(Dictionary<string, object> dictionary, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(status)
            {
                Content = new ObjectContent<Dictionary<string, object>>(dictionary, new JsonMediaTypeFormatter(), "application/json")
            };
        }

        /// <summary>
        /// Creates a simple JSON message with a provided object
        /// </summary>
        /// <param name="obj">The object to serialize to JSON</param>
        /// <param name="status">The status code for this message. Default = OK</param>
        /// <returns>A HttpResponseMessage formatted as JSON with content being the object passed in</returns>
        internal static HttpResponseMessage CreateJsonResponse(object obj, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(status)
            {
                Content = new ObjectContent<object>(obj, new JsonMediaTypeFormatter(), "application/json")
            };
        }

        /// <summary>
        /// Creates an ID table that is used within the database when passing more then 1 id is desired
        /// </summary>
        /// <param name="ids">An array of integers to be used as IDS</param>
        /// <returns>A DataTable that has a single column (ID) with the ids passed in as rows</returns>
        internal static DataTable CreateIDTable(int[] ids)
        {
            var idTable = new DataTable();
            idTable.Columns.Add("ID", typeof(int));
            foreach (int id in ids)
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
        internal static T readObject<T>(SqlDataReader reader, string column, T defaultValue)
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
        internal static Nullable<T> readObjectOrNull<T>(SqlDataReader reader, string column) where T : struct
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