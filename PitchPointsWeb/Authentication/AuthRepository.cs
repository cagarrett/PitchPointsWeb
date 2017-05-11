using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PitchPointsWeb.API;
using PitchPointsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PitchPointsWeb.Authentication
{
    public class AuthRepository : IDisposable
    {

        private AuthContext context;

        public AuthRepository()
        {
            context = new AuthContext();
        }

        public async Task<IdentityResult> RegisterUser(RegisterAPIUser user)
        {
            var store = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(store);
            var appUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Email
            };
            return await userManager.CreateAsync(appUser, user.Password);
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}