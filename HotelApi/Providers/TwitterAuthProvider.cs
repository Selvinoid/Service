using System.Security.Claims;
using System.Threading.Tasks;
using Duke.Owin.VkontakteMiddleware.Provider;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;

namespace HotelApi.Providers
{
    public class TwitterAuthProvider : TwitterAuthenticationProvider
    {
        public override Task Authenticated(TwitterAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public override void ApplyRedirect(TwitterApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public override Task ReturnEndpoint(TwitterReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }

}