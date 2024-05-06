using CompuSPED.Utils;
using System.Web.Http;

namespace CompuSPED
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();
            UnityConfig.RegisterComponents();
            config.MessageHandlers.Add(new TokenValidationHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
