using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using PitchPointsWeb.Models.API;
using PitchPointsWeb.API;

namespace PitchPointsWeb
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //read the token
            //string token = ReadToken();

            //create token model
            //var model = new TokenModel(token);

            //set the token

            //validate

            //set logged in view

        }

        public void WriteToken(string token)
        {
            Response.Cookies.Add(new HttpCookie("Token") { Value = token });
        }

        public string ReadToken()
        {
            return Request.Cookies["Token"]?.Value;
        }

        public async Task<bool> IsValidLogin()
        {
            var token = ReadToken();
            if (token == null) return false;
            var tokenModel = new TokenModel() { Token = token };
            var isValid = await tokenModel.Validate();
            WriteToken(isValid ? tokenModel.Token : null);
            return isValid;
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async void PopupLogin(LoginModel model)
        {
            var validLogin = await new AccountController().Login(model);

            if (validLogin.Success)
            {
                WriteToken(validLogin.Token);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                int responseCode = validLogin.ResponseCode;
                switch (responseCode)
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

        public async void login_Click(object sender, EventArgs e)
        {
            var validLogin = await new AccountController().Login(new LoginModel { Email = user_email.Value, Password = user_password.Value });
            
            if (validLogin.Success)
            {
                WriteToken(validLogin.Token);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                int responseCode = validLogin.ResponseCode;
                switch (responseCode)
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