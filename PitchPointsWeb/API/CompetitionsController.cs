using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PitchPointsWeb.Models;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.Models.API.Response;
using static System.Int32;

namespace PitchPointsWeb.API
{
    public class CompetitionsController : MasterController
    {

        [HttpGet]
        public CompetitionsResponse Get()
        {
            return GetCompetitionsFor("");
        }

        [HttpPost]
        public async Task<CompetitionsResponse> Get([FromBody] TokenModel data)
        {
            var valid = await data.Validate();
            CompetitionsResponse response;
            if (valid)
            {
                response = GetCompetitionsFor(data.Content.Email);
                response.Token = data.Token;
            }
            else
            {
                response = ApiResponseCode.AuthError.ToResponse<CompetitionsResponse>();
            }
            return response;
        }

        [HttpPost]
        public async Task<CompetitionRegistrationResponse> ModifyCompetitionStatus([FromBody] CompetitionRegistrationModel model)
        {
            var valid = await model.Validate();
            CompetitionRegistrationResponse response;
            if (valid)
            {
                response = ChangeRegistrationStatus(model);
                response.Token = model.Token;
            }
            else
            {
                response = ApiResponseCode.AuthError.ToResponse<CompetitionRegistrationResponse>();
            }
            return response;
        }

        private static CompetitionRegistrationResponse ChangeRegistrationStatus(CompetitionRegistrationModel model)
        {
            var response = new CompetitionRegistrationResponse();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("ModifyRegistrationStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = model.Content.Email;
                        command.Parameters.Add("@compId", SqlDbType.Int).Value = model.CompetitionId;
                        command.Parameters.Add("@register", SqlDbType.Bit).Value = model.Register;
                        var outClimber = new SqlParameter("@climberId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var outResponse = new SqlParameter("@responseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(outClimber);
                        command.Parameters.Add(outResponse);
                        command.ExecuteNonQuery();
                        response.CompetitionId = model.CompetitionId;
                        response.ClimberId = (int)outClimber.Value;
                        response.IsRegistered = (response.ClimberId != 0);
                        var responseCode = (int) outResponse.Value;
                        response.ApiResponseCode = responseCode.ParseCompetitionRegistrationCode();
                    }
                }
            }
            catch
            {
                response.ApiResponseCode = ApiResponseCode.InternalError;
            }
            return response;
        }

        private static CompetitionsResponse GetCompetitionsFor(string email)
        {
            var response = new CompetitionsResponse();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetActiveCompetitions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Competitions.Add(ReadCompetition(reader));
                            }
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

        internal static Competition ReadCompetition(SqlDataReader reader)
        {
            var comp = new Competition
            {
                CompetitionTitle = ReadObject(reader, "CompTitle", ""),
                Details = ReadObject(reader, "CompDetails", ""),
                Description = ReadObject(reader, "Description", ""),
                IsRegistered = ReadObject(reader, "IsRegistered", 0) == 1,
                Climbers = ReadObject(reader, "Climbers", 0)
            };
            var id = ReadObjectOrNull<int>(reader, "ID");
            if (id.HasValue)
            {
                comp.Id = id.Value;
            }
            var startDate = ReadObjectOrNull<DateTime>(reader, "StartDate");
            var endDate = ReadObjectOrNull<DateTime>(reader, "EndDate");
            if (startDate.HasValue)
            {
                comp.StartDate = startDate.Value;
            }
            if (endDate.HasValue)
            {
                comp.EndDate = endDate.Value;
            }
            comp.Location = ReadLocation(reader);
            foreach (var rule in ReadRules(reader))
            {
                comp.AddRule(rule);
            }
            foreach (var category in ReadCategories(reader))
            {
                comp.AddCategory(category);
            }
            foreach (var type in ReadTypes(reader))
            {
                comp.AddType(type);
            }
            return comp;
        }

        internal static Location ReadLocation(SqlDataReader reader)
        {
            var location = new Location
            {
                Nickname = ReadObject(reader, "LocationNickname", ""),
                State = ReadObject(reader, "State", ""),
                City = ReadObject(reader, "City", ""),
                ZIP = ReadObject(reader, "Zip", ""),
                AddressLine1 = ReadObject(reader, "AddressLine1", ""),
                AddressLine2 = ReadObject(reader, "AddressLine2", ""),
                GooglePlaceId = ReadObject(reader, "GooglePlaceId", "")
            };
            var id = ReadObjectOrNull<int>(reader, "LocationID");
            if (id.HasValue)
            {
                location.Id = id.Value;
            }
            return location;
        }

        internal static List<CompetitionRule> ReadRules(SqlDataReader reader)
        {
            var list = new List<CompetitionRule>();
            var stringList = ReadObject(reader, "Rules", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (var pair in stringList)
            {
                var rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionRule();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (TryParse(rulePair[0], out id))
                {
                    compRule.Id = id;
                }
                compRule.Description = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

        internal static List<CompetitionCategory> ReadCategories(SqlDataReader reader)
        {
            var list = new List<CompetitionCategory>();
            var stringList = ReadObject(reader, "Categories", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (var pair in stringList)
            {
                var rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionCategory();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (TryParse(rulePair[0], out id))
                {
                    compRule.Id = id;
                }
                compRule.Name = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

        internal static List<CompetitionType> ReadTypes(SqlDataReader reader)
        {
            var list = new List<CompetitionType>();
            var stringList = ReadObject(reader, "Types", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (var pair in stringList)
            {
                var rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionType();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (TryParse(rulePair[0], out id))
                {
                    compRule.Id = id;
                }
                compRule.Type = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

    }
}
