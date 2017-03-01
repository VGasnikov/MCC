using System.IO;
using System;

namespace MCC.Domain
{
    public class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string Template { get; set; }
        public string Language { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }

        public string Properties
        {
            get
            {
                var w = new StringWriter();
                try
                {
                    var type = Type.GetType(Class, true);
                    foreach (var t in type.GetProperties())
                        w.Write(t.Name + ", ");
                }
                catch { }
                return w.ToString();
            }
        }
        public object GetObjectById(string id)
        {
            var type = Type.GetType(Class, true);
            var o = (Email.IEmailDataModel)Activator.CreateInstance(type);
            //var o = (Email.IEmailDataModel)type.get.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(int), typeof(object) }, null).Invoke(new object[] { });
            var obj = o.GetObjectById(id, Language);
            return obj;
        }
    }


}