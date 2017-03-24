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
            if(first_name.Value != "" && last_name.Value != "" && date_of_birth.Value != "" && password.Value != "" && email.Value != "")
            {
                if (password.Value == confirm_password.Value)
                {
                    var registerModel = new RegisterModel
                    {
                        Email = email.Value,
                        Password = password.Value,
                        FirstName = first_name.Value,
                        LastName = last_name.Value,
                        DateOfBirth = DateTime.Parse(date_of_birth.Value)
                    };

                    var validRegister = await new AccountController().Register(registerModel);
                    if (validRegister.Success)
                    {
                        Master.WriteToken(validRegister.Token);
                        Response.Redirect("../Default.aspx", false);
                    }
                    else
                    {
                        int responseCode = validRegister.ResponseCode;
                        switch (responseCode)
                        {
                            case 1:
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "authError();", true);
                                break;
                            case 2:
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "serverError();", true);
                                break;
                            case 100:
                                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "emailError();", true);
                                break;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "passwordMismatch();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "completeForm();", true);
            }
        }
    }
}