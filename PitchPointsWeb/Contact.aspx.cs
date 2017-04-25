using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;


namespace PitchPointsWeb
{
    public partial class Contact : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string empty = "";
            if(!first_name.Value.Equals(empty) && !last_name.Value.Equals(empty) && !message_subject.Value.Equals(empty) && !message_content.Value.Equals(empty))
            {
                var user = first_name.Value + " " + last_name.Value;
                var fromAddress = new MailAddress("PitchPointsIssueTracking@gmail.com", user);
                var toAddress = new MailAddress("PitchPointsTeam@gmail.com", "Pitch Points Team");
                const string fromPassword = "PitchPointsBugraCodyRiderSam";
                var subject = message_subject.Value;
                var body = message_content.Value;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    first_name.Value = "";
                    last_name.Value = "";
                    message_subject.Value = "";
                    message_content.Value = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "success();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "completeForm();", true);
            }
        }
    }
}