using System;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;
using PitchPointsWeb.Models.API;

namespace PitchPointsWeb.Account
{
    public partial class Profile : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Master.ReadToken();

            
            string empty = "";
            if (EmailLabel.Text == empty)
            {
                var controller = new AccountController();
                var TokenModel = new TokenModel
                {
                  Token = Master.ReadToken()
                };
                var result = await controller.GetUserSnapshot(TokenModel);
                if (result.Success)
                {
                    LifeTimePointsLabel.Text = result.Points.ToString();
                    FallsLabel.Text = result.Falls.ToString();
                    ParticipatedCompsLabel.Text = result.ParticipatedCompetitions.ToString();
                }
                else
                {

                }
            }

        }
    }
}