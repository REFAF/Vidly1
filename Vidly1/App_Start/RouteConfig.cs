using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //Attribute routing (13)
            // first: enable it. (13.1) 
            routes.MapMvcAttributeRoutes(); // call method. then do to MoviesController


            // Custom route (12)
            // The order of routes is matter. define them from the most specific to most generic. (12)
            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "movies/released/{year}/{month}", //{parameter}
            //    new { controller = "Movies", action = "ByReleaseDate"},
            //    new { year = @"\d{4}", month = @"\d{2}"} // 4 digit for year, 2 digit  for month (11.2)
            //    // to limit the year -> year = @"2015|2016" (11.2)
            //    );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
