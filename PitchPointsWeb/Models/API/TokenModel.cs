using System.Threading.Tasks;
using PitchPointsWeb.API;

namespace PitchPointsWeb.Models.API
{
    public class PrivateRequestModel
    {

        public string Token { get; set; }

        public async Task<bool> IsValid()
        {
            var controller = new MasterController();
            var status = await controller.VerifyToken(Token);
            return status;
        }

    }
}