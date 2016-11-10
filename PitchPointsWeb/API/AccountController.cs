using PitchPointsWeb.Models;
using System.Web.Http;

namespace PitchPointsWeb.API
{
    public class AccountController : ApiController
    {

        public void RegisterAccount([FromBody] RegisterAPIUser user)
        {

        }

        public void Login([FromBody] LoginAPIUser value)
        {

        }

        public void ResetPassword([FromBody] string value)
        {

        }

    }
}
