using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{

    public class RegisterAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public RegisterAccountResponse()
        {

        }

    }

    public class LoginAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public LoginAccountResponse()
        {

        }

    }

    public class PrivateKeyInfo
    {

        public string PrivateKey { get; set; }

        public int PublicKeyId { get; set; }

        public PrivateKeyInfo()
        {

        }

    }

}