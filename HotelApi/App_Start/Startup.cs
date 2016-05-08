
using Duke.Owin.VkontakteMiddleware;
using HotelApi.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security.Twitter;

[assembly: OwinStartup(typeof(MoodronWebApi.App_Start.Startup))]

namespace MoodronWebApi.App_Start
{
    using System;
    using System.Web.Http;

    using HotelApi;

    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
        public static MicrosoftAccountAuthenticationOptions MicrosoftAccountAuthenticationOptions { get; private set; }
        public static TwitterAuthenticationOptions TwitterAuthenticationOptions { get; private set; }   

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "542956725433-tjt2mdu5of5qalbk26phenan00e2kdgk.apps.googleusercontent.com",
                ClientSecret = "TiLvtEVkNEVCBkpR6in2mvND",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleAuthOptions);

            //Configure Facebook External Login
            FacebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "240497109671640",
                AppSecret = "ecf1eb0f4e29979fb4c1691da4d31045",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(FacebookAuthOptions);

            TwitterAuthenticationOptions = new TwitterAuthenticationOptions
            {
                ConsumerKey = "	oQLf0BColX7994mRbWwphZcol",
                ConsumerSecret = "h044zlFw7VYHiqb8r3GMSX6v5EApGDpWlHPMKScqACWQeKcq5C"
            };
            app.UseTwitterAuthentication(TwitterAuthenticationOptions);

        }
    }
}