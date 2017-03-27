using System.Linq;
using System.Web.Http;
using PitchPointsWeb.Models;
using System.Data.SqlClient;
using PitchPointsWeb.Models.API.Response;
using System.Threading.Tasks;

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
                            response.Leaderboard = leaderboard;
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

        [HttpPost]
        public async Task<ScorecardResponse> GetScorecard(ScorecardRequest request)
        {
            var valid = await request.Validate();
            if (!valid)
            {
                return ApiResponseCode.AuthError.ToResponse<ScorecardResponse>();
            }
            var response = new ScorecardResponse();
            response.Token = request.Token;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetScorecard", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@email", request.Content.Email);
                        command.Parameters.AddWithValue("@compId", request.CompetitionId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    response.Entries.Add(ReadScorecardEntry(reader));
                                }
                            }
                        }
                    }
                }
            } catch
            {
                response.ApiResponseCode = ApiResponseCode.InternalError;
            }
            return response;
        }

        private static Leaderboard ReadLeaderboard(SqlDataReader reader)
        {
            var leaderboard = new Leaderboard();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    leaderboard.Entries.Add(ReadLeaderboardEntry(reader));
                }
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

        private static ScorecardEntry ReadScorecardEntry(SqlDataReader reader)
        {
            return new ScorecardEntry
            {
                RouteID = ReadObject(reader, "RouteID", 0),
                WitnessID = ReadObject(reader, "WitnessID", 0),
                WitnessName = ReadObject(reader, "WitnessName", ""),
                Falls = ReadObject(reader, "Falls", 0),
                Points = ReadObject(reader, "Points", 0)
            };
        }

    }

}
