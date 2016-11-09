using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PitchPointsWeb.Startup))]
namespace PitchPointsWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
