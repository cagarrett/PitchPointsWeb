using System;
using System.Data;
using System.Data.SqlClient;

namespace PitchPointsWeb.API
{
    public class APICommon
    {

        internal static SqlConnection GetConnection()
        {
            var connection = new SqlConnection();
            connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PitchPointsDB"].ConnectionString;
            return connection;
        }

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