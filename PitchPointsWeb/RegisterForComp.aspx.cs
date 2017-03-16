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
    public partial class RegisterForComp : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var user = FName.Text + " " + LName.Text;
            var fromAddress = new MailAddress("PitchPointsIssueTracking@gmail.com", user);
            var toAddress = new MailAddress("PitchPointsTeam@gmail.com", "Pitch Points Team");
            const string fromPassword = "PitchPointsBugraCodyRiderSam";
            var subject = Subject.Text;
            var body = Message.Text;

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
                FName.Text = "";
                LName.Text = "";
                Subject.Text = "";
                Message.Text = "";

                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Your message has been sent to the Pitch Points Support Team.";
            }
        }
    }
}