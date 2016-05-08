using System.Security.Claims;
using System.Threading.Tasks;
using Duke.Owin.VkontakteMiddleware.Provider;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;

namespace HotelApi.Providers
{
    public class MicrosoftAuthProvider : MicrosoftAccountAuthenticationProvider
    {
        public override Task Authenticated(MicrosoftAccountAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public override void ApplyRedirect(MicrosoftAccountApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public override Task ReturnEndpoint(MicrosoftAccountReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }

}