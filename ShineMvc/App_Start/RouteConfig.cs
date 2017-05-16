#region Usings

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace ShineMvc
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("NotFound", "NotFound", new { controller = "Error", action = "NotFound" });

            routes.MapRoute("ServerError", "ServerError", new { controller = "Error", action = "ServerError" });

            routes.MapRoute("EmailWelcome", "welcome/{email}/{version}/{videoFriendlyUrl}", new { controller = "Welcome", action = "Index" });

            routes.MapRoute("VideoRoute", "{version}/{friendlyUrl}", new { controller = "Home", action = "Video" });

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
        }
    }
}