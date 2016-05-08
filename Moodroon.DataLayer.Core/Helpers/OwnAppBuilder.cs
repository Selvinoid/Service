using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Models;
using DataAccessLayer.Repositories.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Moodroon.DataLayer.Core.Helpers
{
    public class OwnAppBuilder
    {

        public static void Create(IAppBuilder app)
        {

        }

        public static CookieAuthenticationOptions CreateCookieAuthenticationOptions()
        {
            return new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider =
                               new CookieAuthenticationProvider
                               {
                                   // Enables the application to validate the security stamp when the user logs in.
                                   // This is a security feature which is used when you change a 
                                   // password or add an external login to your account.  
                                   OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager,
                                           User, int>(validateInterval: TimeSpan.FromMinutes(30),
                                               regenerateIdentityCallback: async (manager, user) =>
                                            await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie),
                                               // Need to add THIS line because we added the third type argument (int) above:
                                               getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))
                               }
            };
        }
    }
}
