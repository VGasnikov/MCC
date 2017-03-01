using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using MVCGrid.Web;

namespace MCC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string cnStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        public static string cnMCC = System.Configuration.ConfigurationManager.ConnectionStrings["MCCDB"].ConnectionString;

        protected void Application_Start()
        {
            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
