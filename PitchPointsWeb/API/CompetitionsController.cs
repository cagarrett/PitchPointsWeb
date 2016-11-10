using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Formatting;
using System;
using PitchPointsWeb.Models;
using static PitchPointsWeb.API.APICommon;

namespace PitchPointsWeb.API
{
    public class CompetitionsController : ApiController
    {

        public HttpResponseMessage Get()
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
            } catch (SqlException e)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
            }
            var data = new List<Competition>();
            using (var command = new SqlCommand("GetActiveCompetitions", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Competition comp = readCompetition(reader);
                    data.Add(comp);
                }
                reader.Close();
            }
            connection.Close();
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new ObjectContent<List<Competition>>(data, new JsonMediaTypeFormatter(), "application/json");
            return response;
        }

        private Competition readCompetition(SqlDataReader reader)
        {
            var comp = new Competition();
            int? id = readObjectOrNull<int>(reader, "ID");
            if (id.HasValue)
            {
                comp.ID = id.Value;
            }
            comp.CompetitionTitle = readObject(reader, "CompTitle", "");
            comp.Details = readObject(reader, "CompDetails", "");
            comp.Description = readObject(reader, "Description", "");
            DateTime? startDate = readObjectOrNull<DateTime>(reader, "StartDate");
            DateTime? endDate = readObjectOrNull<DateTime>(reader, "EndDate");
            if (startDate.HasValue)
            {
                comp.StartDate = startDate.Value;
            }
            if (endDate.HasValue)
            {
                comp.EndDate = endDate.Value;
            }
            comp.Location = readLocation(reader);
            foreach (CompetitionRule rule in readRules(reader))
            {
                comp.AddRule(rule);
            }
            foreach (CompetitionCategory category in readCategories(reader))
            {
                comp.AddCategory(category);
            }
            foreach (CompetitionType type in readTypes(reader))
            {
                comp.AddType(type);
            }
            return comp;
        }

        private Location readLocation(SqlDataReader reader)
        {
            var location = new Location();
            var id = readObjectOrNull<int>(reader, "LocationID");
            if (id.HasValue)
            {
                location.ID = id.Value;
            }
            location.Nickname = readObject(reader, "LocationNickname", "");
            location.State = readObject(reader, "State", "");
            location.City = readObject(reader, "City", "");
            location.ZIP = readObject(reader, "Zip", "");
            location.AddressLine1 = readObject(reader, "AddressLine1", "");
            location.AddressLine2 = readObject(reader, "AddressLine2", "");
            location.GooglePlaceId = readObject(reader, "GooglePlaceId", "");
            return location;
        }

        private List<CompetitionRule> readRules(SqlDataReader reader)
        {
            var list = new List<CompetitionRule>();
            var stringList = readObject(reader, "Rules", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (string pair in stringList)
            {
                string[] rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionRule();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (Int32.TryParse(rulePair[0], out id))
                {
                    compRule.ID = id;
                }
                compRule.Description = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

        private List<CompetitionCategory> readCategories(SqlDataReader reader)
        {
            var list = new List<CompetitionCategory>();
            var stringList = readObject(reader, "Categories", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (string pair in stringList)
            {
                string[] rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionCategory();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (Int32.TryParse(rulePair[0], out id))
                {
                    compRule.ID = id;
                }
                compRule.Name = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

        private List<CompetitionType> readTypes(SqlDataReader reader)
        {
            var list = new List<CompetitionType>();
            var stringList = readObject(reader, "Types", "").Split(new[] { "++" }, StringSplitOptions.None);
            foreach (string pair in stringList)
            {
                string[] rulePair = pair.Split(new[] { "||" }, StringSplitOptions.None);
                int id;
                var compRule = new CompetitionType();
                if (rulePair.Length < 2)
                {
                    continue;
                }
                if (Int32.TryParse(rulePair[0], out id))
                {
                    compRule.ID = id;
                }
                compRule.Type = rulePair[1];
                list.Add(compRule);
            }
            return list;
        }

    }
}
