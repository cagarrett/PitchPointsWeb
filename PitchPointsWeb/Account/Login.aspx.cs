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
                Session["Username"] = Email.Text;
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            } else {
                string responseMessage = validLogin.ResponseMessage.ToString();
                switch(responseMessage)
                {
                    case "Error authorizing user":
                        MessageBox.Show("Error authorizing this user.", "Error authorizing user", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                    case "Internal server error":
                        MessageBox.Show("Error with the internal server.", "Internal Server Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                    case "User already registered with this email address":
                        MessageBox.Show("User already registered with this email address.", "Email Taken", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                    case "Incorrect password":
                        MessageBox.Show("The password you entered does not match the username provided.", "Incorrect Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                    case "The email address does not exist":
                        MessageBox.Show("The email address does not exist.", "Incorrect Email Address", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                }
            }
        }
    }
}