using System;
using System.Web;
using System.Web.UI;
using PitchPointsWeb.API;
using PitchPointsWeb.Models.API;
using System.Windows;

namespace PitchPointsWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected async void LogIn(object sender, EventArgs e)
        {
            var validLogin = await new AccountController().Login(new LoginModel { Email = Email.Text, Password = Password.Text });

            if (validLogin.Success)
            {
                Master.WriteToken(validLogin.Token);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            } else {
                int responseCode = validLogin.ResponseCode;
                switch(responseCode)
                {
                    case 1:
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "authError();", true);
                    break;
                    case 2:
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "serverError();", true);
                    break;
                    case 102:
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "incorrectPassword();", true);
                    break;
                    case 101:
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "accountDoesntExist();", true);
                    break;
                }
            }
        }
    }
}