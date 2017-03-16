using System;
using System.Threading.Tasks;
using PitchPointsWeb.API;

namespace PitchPointsWeb.Models.API
{
    public class TokenModel
    {

        public string Token { get; set; }

        public async Task<TokenContentModel> ToContent()
        {
            var controller = new MasterController();
            var model = await controller.ConvertTokenToModel(Token);
            return model;
        }

    }

    public class TokenContentModel
    {
        
        public string Email { get; set; }

        public DateTime ExpDateTime { get; set; }

        public TokenContentModel()
        {
            Email = "";
            ExpDateTime = DateTime.MinValue;
        }

        public bool IsValid()
        {
            return (ExpDateTime - DateTime.Now).TotalHours > 1.0;
        }

    }

}