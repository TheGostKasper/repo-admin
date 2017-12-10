using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(name: "admin", url: "admin", defaults: new { controller = "Home", action = "Admin" });
            routes.MapRoute(name: "general", url: "general", defaults: new { controller = "Home", action = "General" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Blog",
                url: "home/blog/preview/{title}",
                defaults: new { controller = "Home", action = "preview", title = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Edit",
                url: "home/blog/edit/{title}",
                defaults: new { controller = "Home", action = "blog", title = UrlParameter.Optional }
            );
        }
    }
}
