using System;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;
using PitchPointsWeb.Models.API;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace PitchPointsWeb.Admin
{
    public partial class LogRouteForClimber : AdminPage
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

        protected async void btnSubmit_Click(object sender, EventArgs e)
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
                var result = await controller.LogClimb(logClimbModel);
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