﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrashCollector
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            /*RouteCollectionAttributeRoutingExtensions.MapMvcAttributeRoutes();*/

            /*routes.MapRoute(
                name: "FilterByDay",
                url: "{controller}/{action}/{day}",
                defaults: new { controller = "Home", action = "Index", day = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
