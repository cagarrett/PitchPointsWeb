using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using PitchPointsWeb.Models;

namespace PitchPointsWeb.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        private void writeCookies(PrivateKeyInfo info)
        {
            HttpCookie privateKeyString = new HttpCookie("PrivateKeyId");
            HttpCookie publicKeyString = new HttpCookie("PublicKeyId");

            // Set the cookie value.
            privateKeyString.Value = info.KeyAsBase64();
            publicKeyString.Value = info.PublicKeyId.ToString();

            // Set the cookie expiration date.
            //DateTime now = DateTime.Now;
            //privateKeyString.Expires = now.AddYears(50); // For a cookie to effectively never expire

            // Add the cookie.
            Response.Cookies.Add(privateKeyString);
            Response.Cookies.Add(publicKeyString);
        }

        private void readCookies(PrivateKeyInfo info)
        {
            HttpCookie privateKeyCookie = Request.Cookies["PrivateKeyId"];
            HttpCookie publicKeyCookie = Request.Cookies["PublicKeyCookie"];

            // Read the cookie information and display it.
            if (privateKeyCookie != null)
                Response.Write("<p>" + privateKeyCookie.Name + "<p>" + privateKeyCookie.Value);
            else
                Response.Write("not found");

            if (publicKeyCookie != null)
                Response.Write("<p>" + publicKeyCookie.Name + "<p>" + publicKeyCookie.Value);
            else
                Response.Write("not found");
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                var controller = new API.AccountController();
                var login = new LoginAPIUser();

                switch (result)
                {
                    case SignInStatus.Success:
                        controller.InternalLogin(login);
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt";
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}