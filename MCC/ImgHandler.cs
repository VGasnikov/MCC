using System.Web;
using System.IO;

namespace MCC
{
    public class ImgHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var file=context.Request.Params["id"];
            context.Response.ContentType = "image/jpeg";
            //context.Response.AddHeader("Accept-Ranges", "bytes");            
            string fileName = System.Configuration.ConfigurationManager.AppSettings["ImgRootURL"] + file;
            if (File.Exists(fileName))
            {
                context.Response.WriteFile(fileName);
                var fi = new FileInfo(fileName);
                context.Response.AddHeader("content-disposition", "inline; filename="+fi.Name);
            }
            else
                context.Response.WriteFile(HttpContext.Current.Server.MapPath("~/Img/cancel.png"));
        }
    }


}