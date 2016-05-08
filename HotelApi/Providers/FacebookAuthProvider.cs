using System.Security.Claims;
using System.Threading.Tasks;
using Duke.Owin.VkontakteMiddleware.Provider;
using Microsoft.Owin.Security.Facebook;

namespace HotelApi.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public override void ApplyRedirect(FacebookApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public override Task ReturnEndpoint(FacebookReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}