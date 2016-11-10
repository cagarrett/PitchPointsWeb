using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using PitchPointsWeb.Models;
using static PitchPointsWeb.API.APICommon;

namespace PitchPointsWeb.API
{
    public class RouteController : ApiController
    {

        public HttpResponseMessage GetRoute(int id)
        {
            return GetRouteInformation(new int[] { id });
        }

        public HttpResponseMessage GetRouteInformation([FromBody] int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var connection = GetConnection();
            try
            {
                connection.Open();
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            var idTable = APICommon.CreateIDTable(ids);
            var routes = new List<PublicRoute>();
            HttpResponseMessage response = null;
            using (var command = new SqlCommand("GetRouteInformation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter errorCode = new SqlParameter("@ErrorCode", SqlDbType.Int);
                errorCode.Direction = ParameterDirection.Output;
                SqlParameter errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200);
                errorMessage.Direction = ParameterDirection.Output;
                command.Parameters.Add(errorCode);
                command.Parameters.Add(errorMessage);
                SqlParameter param = command.Parameters.AddWithValue("@IDList", idTable);
                param.SqlDbType = SqlDbType.Structured;
                SqlDataReader reader = command.ExecuteReader();
                if ((int) (errorCode.Value ?? 0) != 0)
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(errorMessage.Value.ToString());
                } else
                {
                    while (reader.Read())
                    {
                        routes.Add(readRoute(reader));
                    }
                }
                reader.Close();
            }
            connection.Close();
            if (response == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent<List<PublicRoute>>(routes, new JsonMediaTypeFormatter(), "application/json");
            }
            return response;
        }

        [HttpPost]
        public HttpResponseMessage GetCompetitionRoutes([FromBody] int competitionID)
        {
            var connection = GetConnection();
            try
            {
                connection.Open();
            } catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            var routes = new List<PublicRoute>();
            using (var command = new SqlCommand("GetCompetitionRoutes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CompetitionID", competitionID);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    routes.Add(readRoute(reader));
                }
                reader.Close();
            }
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<PublicRoute>>(routes, new JsonMediaTypeFormatter(), "application/json")
            };
        }

        private PublicRoute readRoute(SqlDataReader reader)
        {
            var route = new PublicRoute();
            int? id = readObjectOrNull<int>(reader, "Id");
            if (id.HasValue)
            {
                route.ID = id.Value;
            }
            route.Name = readObject(reader, "RouteName", "No route name");
            route.CategoryName = readObject(reader, "Category", "No category");
            route.MaxPoints = readObject(reader, "MaxPoints", 0);
            route.PointDeductionPerFall = readObject(reader, "FailurePointDeduction", 0);
            return route;
        }

    }

}
