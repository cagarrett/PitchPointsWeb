using System;
using System.Web;
using System.Web.UI;
using PitchPointsWeb.API;
using PitchPointsWeb.Models.API;
using System.Data.SqlClient;
using System.Data;

namespace PitchPointsWeb
{
    public partial class Log_A_Climb : BasePage
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
        string climberid;

        protected async void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                if (Master.ReadToken() == null)
                {
                    Response.Redirect("~/", false);
                    return;
                }
                var tokenModel = new TokenModel()
                {
                    Token = Master.ReadToken()
                };
                var loggedIn = await tokenModel.Validate();
                if (loggedIn)
                {
                    using (var connection = MasterController.GetConnection())
                    {
                        connection.Open();
                        using (var command = new SqlCommand("GetAllUserInfo", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@email", tokenModel.Content.Email);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                FirstLabel.Text = (UppercaseFirst(rdr["FirstName"].ToString()));
                                LastLabel.Text = (UppercaseFirst(rdr["LastName"].ToString()));
                                
                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }
                    /*
                    using (var connection = MasterController.GetConnection())
                    {
                        connection.Open();
                        using (var command = new SqlCommand("GetClimbersInCompetition", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@compID", tokenModel.Content.Email);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                climberid = (rdr["UserID"].ToString());
                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }*/

                    getActiveCompetitions.SelectParameters.Add("email", tokenModel.Content.Email);
                    getActiveCompetitions.SelectParameters.Add("onlyReturnRegistered", "1");
                    getActiveCompetitions.DataBind();
                    competitionName.DataBind();
                    getCompetitionRoutes.SelectParameters.Add("CompetitionID", competitionName.SelectedValue);
                    getCompetitionRoutes.DataBind();
                }
                else
                {
                    Response.Redirect("~/", false);
                }
            }
        }
        

        protected void competitionChanged(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(competitionName.SelectedValue);
            getCompetitionRoutes.SelectParameters["CompetitionID"].DefaultValue = Id.ToString();
            getCompetitionRoutes.DataBind();
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)

        {
            string empty = "";
            if (falls.Value != empty)
            {
                var controller = new RouteController();
                var logClimbModel = new LoggedClimbModel
                {
                    ClimberId = Convert.ToInt32(climberid),
                    //WitnessId = Convert.ToInt32(witness_id.Value),
                    //RouteId = Convert.ToInt32(route_id.Value),
                    Falls = Convert.ToInt32(falls.Value),
                };
                var result = await controller.LogClimb(logClimbModel);
                if (result.Success)
                {
                    //climber_id.Value = empty; witness_id.Value = empty; route_id.Value = empty; falls.Value = empty;
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