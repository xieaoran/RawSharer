using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RawSharer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: string.Empty,
                defaults: new { controller = "Home", action = "Base" }
            );
            routes.MapRoute(
                name: "Blobs",
                url: "Blobs/{id}",
                defaults: new { controller = "Blobs", action = "Get" }
            );
        }
    }
}
