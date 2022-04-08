using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanBif.NPS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{id}/{key}/{rating}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, key=UrlParameter.Optional, rating=UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{nuevoCont}/{oficina}",
                defaults: new { controller = "Login", action = "Index", nuevoCont = UrlParameter.Optional, oficina = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "RegistrarVotoNps",
            //    url: "{controller}/{action}/{data}",
            //    defaults: new { controller = "Home", action = "RegistrarVoto", data = UrlParameter.Optional }
            //);
        }
    }
}
