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
                string responseMessage = validLogin.ResponseMessage.ToString();
                switch(responseMessage)
                {
                    case "Error authorizing user":
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "authError();", true);
                    break;
                    case "Internal server error":
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "serverError();", true);
                    break;
                    case "Incorrect password":
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "incorrectPassword();", true);
                    break;
                    case "The email address does not exist":
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "accountDoesntExist();", true);
                    break;
                }
            }
        }
    }
}