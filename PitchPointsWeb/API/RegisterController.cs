using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using PitchPointsWeb.Models;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.Models.API.Response;
using System.Threading.Tasks;

namespace PitchPointsWeb.API
{
    public class RegisterController : MasterController
    {

        [HttpPost]
        public async Task<PrivateApiResponse> RegisterClimber([FromBody] RegisteredClimberModel model)
        {
            var valid = await model.Validate();
            if (!valid)
            {
                return ApiResponseCode.AuthError.ToResponse<PrivateApiResponse>();
            }
            var response = new PrivateApiResponse();
            response.Token = model.Token;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("ModifyRegistrationStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@climberId", model.ClimberId);
                        command.Parameters.AddWithValue("@category", model.Category);
                        command.Parameters.AddWithValue("@compId", model.CompetitionId);
                        //command.Parameters.AddWithValue("@register", 1);
                        var outputCode = new SqlParameter("@errorCode", SqlDbType.Int);
                        outputCode.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputCode);
                        command.ExecuteNonQuery();
                        if (outputCode.Value is int)
                        {
                            response.ApiResponseCode = (ApiResponseCode)outputCode.Value;
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

        /*public RoutesResponse IsRegistered(int id)
        {
            return GetRegistrationInformation(new[] { id });
        }

        public RoutesResponse GetRegistrationInformation([FromBody] int[] ids)
        {
            var response = new RoutesResponse();
            if (ids.Length == 0) return response;
            var idTable = CreateIdTable(ids);
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetRouteInformation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var errorCode = new SqlParameter("@ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(errorCode);
                        var param = command.Parameters.AddWithValue("@IDList", idTable);
                        param.SqlDbType = SqlDbType.Structured;
                        using (var reader = command.ExecuteReader())
                        {
                            if ((int)(errorCode.Value ?? 0) != 0)
                            {
                                response.ApiResponseCode = ApiResponseCode.NoRoutesSupplied;
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    response.Routes.Add(ReadRoute(reader));
                                }
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

        [HttpPost]
        public CompetitionRoutesResponse GetCompetitionRoutes([FromBody] CompetitionRoutesModel model)
        {
            var response = new CompetitionRoutesResponse { CompetitionId = model.CompetitionId };
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SqlCommand("GetCompetitionRoutes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompetitionID", model.CompetitionId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                response.Routes.Add(ReadRoute(reader));
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

        private static PublicRoute ReadRoute(SqlDataReader reader)
        {
            return new PublicRoute
            {
                Id = ReadObject(reader, "Id", 0),
                Name = ReadObject(reader, "RouteName", "No route name"),
                CategoryName = ReadObject(reader, "Category", "No category"),
                MaxPoints = ReadObject(reader, "MaxPoints", 0),
                PointDeductionPerFall = ReadObject(reader, "FailurePointDeduction", 0)
            };
        }*/

    }

}