using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using MCC.Domain;

namespace MCC
{
    public class AudienceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var audienceName = (string)filterContext.RouteData.Values["audience"];
            Audience.Current = new Audience(audienceName??"");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(Audience.Current.Language);
        }
    }
}