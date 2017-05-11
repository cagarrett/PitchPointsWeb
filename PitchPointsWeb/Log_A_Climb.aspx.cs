using System;
using System.Web;
using System.Web.UI;
using PitchPointsWeb.API;
using PitchPointsWeb.Models.API;

namespace PitchPointsWeb
{
    public partial class Log_A_Climb : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
                Response.Redirect("Account/Login.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var controller = new RouteController();
            var logClimbModel = new LoggedClimbModel
            {
                ClimberId = Convert.ToInt32(ClimberID.Text),
                WitnessId = Convert.ToInt32(Witness.Text),
                RouteId = Convert.ToInt32(RouteClimbed.Text),
                Falls = Convert.ToInt32(numberOfFalls.Text),
            };
            var result = controller.LogClimb(logClimbModel);
            if (result.Success)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}