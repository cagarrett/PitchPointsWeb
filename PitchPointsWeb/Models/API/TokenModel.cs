using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PitchPointsWeb.API;

namespace PitchPointsWeb.Models.API
{
    public class TokenModel
    {

        public string Token { get; set; }

        [JsonIgnore]
        internal TokenContent Content { get; set; }

        private async Task ExtractContent()
        {
            if (Content != null) return;
            var controller = new MasterController();
            var dict = await controller.ExtractTokenContent(Token);
            if (dict != null)
            {
                Content = new TokenContent(dict);
            }
        }

        public async Task<bool> Validate()
        {
            await ExtractContent();
            if (Content == null)
            {
                return false;
            }
            Token = await new MasterController().RefreshToken(Content);
            return Token != null;
        }

    }

    public class TokenContent
    {

        public static readonly double ValidTokenDays = 180.0;

        private const string EmailKey = "email";

        private const string ExpiryKey = "exp";

        private const string PasswordHashKey = "passhash";

        public string Email { get; set; }

        public DateTime ExpDateTime { get; set; }

        public string PasswordHash { get; set; }

        public TokenContent()
        {
            UpdateExpiryTime();
        }

        internal TokenContent(Dictionary<string, dynamic> content)
        {
            Email = content[EmailKey];
            ExpDateTime = new DateTime(content[ExpiryKey]);
            PasswordHash = content[PasswordHashKey];
        }

        public void UpdateExpiryTime()
        {
            ExpDateTime = DateTime.Now.AddDays(ValidTokenDays);
        }

        public bool IsValid()
        {
            return (ExpDateTime - DateTime.Now).TotalDays < ValidTokenDays;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { EmailKey, Email },
                { ExpiryKey, ExpDateTime.Ticks},
                { PasswordHashKey, PasswordHash}
            };
        }

    }

}