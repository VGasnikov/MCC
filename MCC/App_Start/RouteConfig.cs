using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MCC.Domain;
namespace MCC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("ImgHandler.ashx/{*pathInfo}");

            var audiences = AudienceRepository.GetAudiences();
            var audienceFilter = string.Join("|", audiences.Select(x => "(" + x + ")"));

            //routes.MapRoute(
            //    name: "Main",
            //    url: "Main/{controller}/{action}/{id}",
            //    defaults: new {action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "DefaultLocalized",
                url: "{audience}/{action}/{id}",
                constraints: new { audience = audienceFilter },   // en or en-US
              //constraints: new { audience = @"(\w{2})|(\w{2}-\w{2})" },   // en or en-US
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
