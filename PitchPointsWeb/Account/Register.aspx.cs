using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;
using System.Threading.Tasks;
using PitchPointsWeb.Models.API;

namespace PitchPointsWeb.Account
{
    public partial class Register : Page
    {

        protected async void CreateUser_Click(object sender, EventArgs e)
        {
            if (WaiverSigned.SelectedValue.Equals("No"))
            {
                var redirect = "<script>window.open('https://app.rockgympro.com/waiver/esign/hoosierheightsindy/f84044fe-27c2-4aae-9052-51d220647d4a');</script>";
                Response.Write(redirect);
                return;
            }
            var registerModel = new RegisterModel
            {
                Email = Email.Text,
                Password = Password.Text,
                FirstName = FName.Text,
                LastName = LName.Text,
                DateOfBirth = DateTime.Parse(DOB.Text)
            };
            var validRegister = await new AccountController().Register(registerModel);
            if (validRegister.Success)
            {
                Master.WriteToken(validRegister.Token);
                Response.Redirect("../Default.aspx", false);
            }
        }
    }
}