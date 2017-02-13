﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;

namespace PitchPointsWeb.Account
{
    public partial class Register : Page
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

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (WaiverSigned.SelectedValue.Equals("No"))
            {
                string redirect = "<script>window.open('https://app.rockgympro.com/waiver/esign/hoosierheightsindy/f84044fe-27c2-4aae-9052-51d220647d4a');</script>";
                Response.Write(redirect);
                return;
            }
            var controller = new AccountController();
            var register = new RegisterAPIUser();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            var registerModel = new RegisterAPIUser
            {
                Email = Email.Text,
                Password = Password.Text,
                FirstName = FName.Text,
                LastName = LName.Text,
                DateOfBirth = DateTime.Parse(DOB.Text)
            };
            var validRegister = new AccountController().InternalRegister(registerModel);
            if (validRegister.Success)
            {
                writeCookies(validRegister.PrivateKey);
                Response.Redirect("../Default.aspx");
            }
            else
            {

            }
        }
    }
}