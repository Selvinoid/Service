namespace HotelApi
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.OAuth;

    using Moodroon.DataLayer.Core;
    using Moodroon.DataLayer.Core.Services;

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly UserService userService;
        public SimpleAuthorizationServerProvider()
        {
            this.userService = new UserService(UnityFactory.Instance.Container);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = await this.userService.AuthenticateApi(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            var roles = await this.userService.GetUserRoles(Convert.ToInt32(user.GetUserId()));
            foreach (var role in roles)
            {
                user.AddClaim(new Claim("role", role));
            }
            context.Validated(user);
        }
    }
}