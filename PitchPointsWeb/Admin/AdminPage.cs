using PitchPointsWeb.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PitchPointsWeb.Admin
{
    public class AdminPage: Page
    {

        protected virtual async void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            var master = Master as SiteMaster;
            if (master?.ReadToken() == null)
            {
                RedirectToHome();
                return;
            }
            var tokenModel = new TokenModel
            {
                Token = master.ReadToken()
            };
            var validToken = await tokenModel.Validate();
            if (!validToken || !tokenModel.Content.IsAdmin())
            {
                RedirectToHome();
            }
        }

        private void RedirectToHome()
        {
            Response.Redirect("~/Default.aspx", false);
        }

    }
}