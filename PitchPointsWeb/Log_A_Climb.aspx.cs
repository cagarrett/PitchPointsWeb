using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;

namespace PitchPointsWeb
{
    public partial class Log_A_Climb : Page
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var controller = new RouteController();
            var logClimb = new LoggedClimbModel();
            var logClimbModel = new LoggedClimbModel
            {
                climberId = Convert.ToInt32(ClimberID.Text),
                witnessId = Convert.ToInt32(Witness.Text),
                routeId = Convert.ToInt32(RouteClimbed.Text),
                falls = Convert.ToInt32(numberOfFalls.Text),
            };
            var validInsert = controller.InsertClimb(logClimbModel);
            if (validInsert.Success)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {

            }
        }
    }
}