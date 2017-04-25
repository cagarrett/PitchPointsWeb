using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using PitchPointsWeb.Models.API;

namespace PitchPointsWeb
{
    public partial class BasePage: Page
    {

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                await DeterminePanel();
            }
        }

        private async Task DeterminePanel()
        {
            var master = (SiteMaster)Master;
            if (master != null)
            {
                await master.DetermineAdminPanel(new TokenModel
                {
                    Token = master.ReadToken()
                });
            }
        }

    }
}