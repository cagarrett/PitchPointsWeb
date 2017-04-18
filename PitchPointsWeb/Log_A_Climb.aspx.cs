using System;
using System.Web;
using System.Web.UI;
using PitchPointsWeb.API;
using PitchPointsWeb.Models.API;
using System.Data.SqlClient;
using System.Data;

namespace PitchPointsWeb
{
    public partial class Log_A_Climb : Page
    {

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            Master.ReadToken();
            //Rider switching to is user logged in boolean

            string empty = "";
            if (FirstName.Text == empty)
            {
                var controller = new AccountController();
                var TokenModel = new TokenModel
                {
                    Token = Master.ReadToken()
                };

                var result = await controller.GetUserSnapshot(TokenModel);
                if (result.Success)
                {
                    using (var connection = MasterController.GetConnection())
                    {
                        connection.Open();
                        using (var command = new SqlCommand("GetAllUserInfo", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@email", TokenModel.Content.Email);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                FirstName.Text = (UppercaseFirst(rdr["FirstName"].ToString()));
                                LastName.Text = (UppercaseFirst(rdr["LastName"].ToString()));
                                //courseNums.Add(rdr["CourseNumber"].ToString());
                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string empty = "";
            if (climber_id.Value != empty && witness_id.Value != empty && route_id.Value != empty && falls.Value != empty)
            {
                var controller = new RouteController();
                var logClimbModel = new LoggedClimbModel
                {
                    ClimberId = Convert.ToInt32(climber_id.Value),
                    WitnessId = Convert.ToInt32(witness_id.Value),
                    RouteId = Convert.ToInt32(route_id.Value),
                    Falls = Convert.ToInt32(falls.Value),
                };
                var result = controller.LogClimb(logClimbModel);
                if (result.Success)
                {
                    climber_id.Value = empty; witness_id.Value = empty; route_id.Value = empty; falls.Value = empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "success();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "serverError();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "completeForm();", true);
            }
        }
    }
}