using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jose;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace PitchPointsWeb.API
{
    public class Authenticator
    {

        public static readonly int VALID_TOKEN_LENGTH = 180;

        private static readonly DateTime EPOCH = new DateTime(1970, 1, 1);

        public static string CreateToken(int userID, string password, byte[] salt)
        {
            var issueDate = DateTime.Now;
            var expireDate = issueDate.AddDays(VALID_TOKEN_LENGTH);
            var payload = new Dictionary<string, object>()
            {
                {"userID", userID.ToString() },
                {"issue", ToUnixTime(issueDate).ToString() },
                {"exp", ToUnixTime(expireDate).ToString() }
            };



            var aes = new AesManaged();
           
            aes.KeySize = 256;
            aes.GenerateKey();

            return JWT.Encode(payload, aes.Key, JweAlgorithm.A256KW, JweEncryption.A256CBC_HS512);
        }

        private static long ToUnixTime(DateTime time)
        {
            return (long)(time.ToUniversalTime().Subtract(EPOCH)).TotalSeconds;
        }

        private static byte[] Base64UrlDecode(string url)
        {
            string s = url;
            s = s.Replace('-', '+');
            s = s.Replace('_', '/');
            switch (s.Length % 4)
            {
                case 0: break;
                case 2:
                    s += "==";
                    break;
                case 3:
                    s += "=";
                    break;
                default:
                    throw new System.Exception("Illegal base64url");
            }
            return Convert.FromBase64String(s);
        }

    }

    public class TokenPair
    {

        public string Token { get; set; }

    }

}