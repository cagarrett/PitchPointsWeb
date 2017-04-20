using PitchPointsWeb.Models;
using PitchPointsWeb.Models.API;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PitchPointsWeb.API.Admin
{
    public class AdminController : MasterController
    {

        public async Task<bool> CreateCompetition(TokenModel token, Competition competition)
        {
            var validToken = await token.Validate();
            if (!validToken || !token.Content.IsAdmin())
            {
                return false;
            }
            var success = false;
            var locationId = CreateLocation(competition.Location);
            if (locationId == -1)
            {
                return false;
            }
            try
            {
                var competitionId = 0;
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("CreateCompetition", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@title", competition.CompetitionTitle);
                        command.Parameters.AddWithValue("@locationId", locationId);
                        command.Parameters.AddWithValue("@details", competition.Details);
                        command.Parameters.AddWithValue("@date", competition.Date);
                        command.Parameters.AddWithValue("@startTime", competition.Time);
                        command.Parameters.AddWithValue("@description", competition.Description);
                        var compIdParam = new SqlParameter()
                        {
                            Direction = System.Data.ParameterDirection.Output,
                            ParameterName = "@compId"
                        };
                        command.Parameters.Add(compIdParam);
                        command.ExecuteNonQuery();
                        competitionId = (int)compIdParam.Value;
                    }
                }
                InsertCategoriesFor(competitionId, competition.Categories.ToList());
                InsertRulesFor(competitionId, competition.Rules.ToList());
                success = true;
            } catch { }
            return success;
        }

        private void InsertRulesFor(int competitionId, List<CompetitionRule> rules)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("InsertRule", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (CompetitionRule rule in rules)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@competitionId", competitionId);
                            command.Parameters.AddWithValue("@description", rule.Description);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            } catch { }
        }

        private void InsertCategoriesFor(int competitionId, List<CompetitionCategory> categories)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("InsertCompetitionCategory", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (CompetitionCategory category in categories)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@competitionId", competitionId);
                            command.Parameters.AddWithValue("@description", category.Name);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch { }
        }

        public int CreateLocation(Location location)
        {
            int locationId = -1;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("CreateLocation", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@nickname", location.Nickname);
                        command.Parameters.AddWithValue("@city", location.City);
                        command.Parameters.AddWithValue("@state", location.State);
                        command.Parameters.AddWithValue("@zip", location.ZIP);
                        command.Parameters.AddWithValue("@addressLine1", location.AddressLine1);
                        command.Parameters.AddWithValue("@addressLine2", location.AddressLine2);
                        var locationParam = new SqlParameter()
                        {
                            ParameterName = "@locationId",
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(locationParam);
                        command.ExecuteNonQuery();
                        locationId = (int) locationParam.Value;
                    }
                }
            } catch { }
            return locationId;
        }

    }
}
