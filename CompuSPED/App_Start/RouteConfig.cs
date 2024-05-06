using System.Web.Mvc;
using System.Web.Routing;

namespace CompuSPED
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "oauth",
               url: "oauth/authorize",
               defaults: new {
                    controller = "Authorization",
                    action = "OAuthAutorize"
               });

            routes.MapRoute(
               name: "oauth_credentials",
               url: "oauth/credentials",
               defaults: new
               {
                   controller = "Authorization",
                   action = "CredentialsValidation"
               });

            routes.MapRoute(
               name: "code",
               url: "oauth/code",
               defaults: new
               {
                   controller = "Authorization",
                   action = "ValidateCode"
               });

            routes.MapRoute(
               name: "saml",
               url: "saml/auth",
               defaults: new
               {
                   controller = "SAMLAuth",
                   action = "SAMLAuthorize"
               });

            routes.MapRoute(
               name: "saml_validation",
               url: "saml/validation",
               defaults: new
               {
                   controller = "SAMLAuth",
                   action = "CredentialValidation"
               });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
