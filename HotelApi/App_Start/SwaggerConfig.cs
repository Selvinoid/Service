using System.Web.Http;
using WebActivatorEx;
using HotelApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace HotelApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                       
                        c.SingleApiVersion("v1", "HotelApi");

                        
                        //
                        //c.BasicAuth("basic")
                        //    .Description("Basic HTTP Authentication");
                        //
                        //c.ApiKey("apiKey")
                        //    .Description("API Key Authentication")
                        //    .Name("apiKey")
                        //    .In("header");
                        //
                        //c.OAuth2("oauth2")
                        //    .Description("OAuth2 Implicit Grant")
                        //    .Flow("implicit")
                        //    .AuthorizationUrl("http://petstore.swagger.wordnik.com/api/oauth/dialog")
                        //    //.TokenUrl("https://tempuri.org/token")
                        //    .Scopes(scopes =>
                        //    {
                        //        scopes.Add("read", "Read access to protected resources");
                        //        scopes.Add("write", "Write access to protected resources");
                        //    });

                      
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}