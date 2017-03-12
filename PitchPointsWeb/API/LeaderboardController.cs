using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PitchPointsWeb.Models;
using System.Data.SqlClient;

namespace PitchPointsWeb.API
{

    public class LeaderboardController : MasterController
    {

        [HttpPost]
        public HttpResponseMessage GetLeaderboard([FromBody] LeaderboardRequest request)
        {
            return CreateJsonResponse(InternalGetLeaderboard(request));
        }


        internal Leaderboard InternalGetLeaderboard(LeaderboardRequest request)
        {
            var connection = GetConnection();
            connection.Open();
            Leaderboard leaderboard = null;
            using (var command = new SqlCommand("GetCategoryLeaderboard", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@catId", request.CategoryId);
                command.Parameters.AddWithValue("@compId", request.CompetitionId);
                var reader = command.ExecuteReader();
                leaderboard = ReadLeaderboard(reader);
                leaderboard.CompetitionId = request.CompetitionId;
                reader.Close();
            }
            connection.Close();
            return leaderboard;
        }

        private Leaderboard ReadLeaderboard(SqlDataReader reader)
        {
            var leaderboard = new Leaderboard();
            leaderboard.Entries = new List<LeaderboardEntry>();
            while (reader.Read())
            {
                leaderboard.Entries.Add(ReadLeaderboardEntry(reader));
            }
            leaderboard.Entries = leaderboard.Entries.OrderByDescending(entry => entry.Points).ToList();
            return leaderboard;
        }

        private LeaderboardEntry ReadLeaderboardEntry(SqlDataReader reader)
        {
            var entry = new LeaderboardEntry();
            entry.CategoryId = readObject(reader, "CategoryId", 0);
            entry.UserId = readObject(reader, "UserId", 0);
            entry.Gender = readObject(reader, "Gender", false);
            entry.FirstName = readObject(reader, "FirstName", "");
            entry.LastName = readObject(reader, "LastName", "");
            entry.Points = readObject(reader, "Points", 0);
            entry.Falls = readObject(reader, "Falls", 0);
            return entry;
        }

    }

}
