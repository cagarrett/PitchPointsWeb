using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Formatting;
using System;
using PitchPointsWeb.Models;

namespace PitchPointsWeb.API
{
    public class CompetitionsController : MasterController
    {

        public HttpResponseMessage Get()
        {
            return GetCompetitionsFor(0);
        }

        [HttpPost]
        public HttpResponseMessage Get(UserIDSignedData data)
        {
            if (data.IsValid())
            {
                return GetCompetitionsFor(data.UserID);
            }
            return GetBadAuthRequest();
        }

        private HttpResponseMessage GetCompetitionsFor(int userId)
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
            var data = new List<Competition>();
            using (var command = new SqlCommand("GetActiveCompetitions", connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Competition comp = readCompetition(reader);
                    data.Add(comp);
                }
                reader.Close();
            }
            connection.Close();
            return CreateJsonResponse(data);
        }

    }
}
