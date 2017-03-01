using System;
using System.Web.Mvc;
using System.Diagnostics;
using MCC.Domain;

namespace MCC.Controllers
{
    public static class CacheExtensions
    {

        public static bool IsModified(this Controller controller, DateTime? updatedAt)
        {
            if (!updatedAt.HasValue)
                return true;
            var headerValue = controller.Request.Headers["If-Modified-Since"];
            if (headerValue == null)
                return true;

            var modifiedSince = DateTime.Parse(headerValue).AddSeconds(1);
            var res = modifiedSince < updatedAt.Value;
            return res;
        }

        public static ActionResult NotModified(this Controller controller)
        {
            return new HttpStatusCodeResult(304, "Page has not been modified");
        }

    }
}