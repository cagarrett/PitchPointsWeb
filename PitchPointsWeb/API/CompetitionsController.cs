using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using PitchPointsWeb.Models;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.Models.API.Response;
using static System.Int32;

namespace PitchPointsWeb.API
{
    public class CompetitionsController : MasterController
    {

        [HttpGet]
        public ApiResponse Get()
        {
            return GetCompetitionsFor(0);
        }

        [HttpPost]
        public ApiResponse Get(UserIDSignedData data)
        {
            return data.IsValid() ? GetCompetitionsFor(data.UserID) : ApiResponseCode.AuthError.ToResponse();
        }

        private static ApiResponse GetCompetitionsFor(int userId)
        {
            var response = new CompetitionsResponse();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetActiveCompetitions", connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
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
                Description = ReadObject(reader, "Description", "")
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
