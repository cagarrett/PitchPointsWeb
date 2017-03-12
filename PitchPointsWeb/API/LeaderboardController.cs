using System.Linq;
using System.Web.Http;
using PitchPointsWeb.Models;
using System.Data.SqlClient;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.Models.API.Response;

namespace PitchPointsWeb.API
{

    public class LeaderboardController : MasterController
    {

        [HttpPost]
        public LeaderboardResponse GetLeaderboard([FromBody] LeaderboardRequest request)
        {
            var response = new LeaderboardResponse();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetCategoryLeaderboard", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@catId", request.CategoryId);
                        command.Parameters.AddWithValue("@compId", request.CompetitionId);
                        using (var reader = command.ExecuteReader())
                        {
                            var leaderboard = ReadLeaderboard(reader);
                            leaderboard.CompetitionId = request.CompetitionId;
                        }
                    }
                }
            }
            catch
            {
                response.ApiResponseCode = ApiResponseCode.InternalError;
            }
            return response;
        }

        private static Leaderboard ReadLeaderboard(SqlDataReader reader)
        {
            var leaderboard = new Leaderboard();
            while (reader.Read())
            {
                leaderboard.Entries.Add(ReadLeaderboardEntry(reader));
            }
            leaderboard.Entries = leaderboard.Entries.OrderByDescending(entry => entry.Points).ToList();
            return leaderboard;
        }

        private static LeaderboardEntry ReadLeaderboardEntry(SqlDataReader reader)
        {
            return new LeaderboardEntry
            {
                CategoryId = ReadObject(reader, "CategoryId", 0),
                UserId = ReadObject(reader, "UserId", 0),
                Gender = ReadObject(reader, "Gender", false),
                FirstName = ReadObject(reader, "FirstName", ""),
                LastName = ReadObject(reader, "LastName", ""),
                Points = ReadObject(reader, "Points", 0),
                Falls = ReadObject(reader, "Falls", 0)
            };
        }

    }

}
