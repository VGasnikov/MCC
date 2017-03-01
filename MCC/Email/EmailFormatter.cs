using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Linq;

namespace MCC.Email
{
    public class EmailFormatter
    {
        private object data;
        private string _template;
        private System.Type type;
        public EmailFormatter (string template, object o)
        {
            data = o;
            _template = template;
            type = o.GetType();
        }

        public static string Format(string template, object o)
        {
            if (string.IsNullOrEmpty(template))
                return "";
            var res = template;
            var ef = new EmailFormatter(template, o);
            res = Regex.Replace(res, @"isvisible=""\[(.*?)\]""", new MatchEvaluator(ef.IsVisibleFormatter), RegexOptions.IgnorePatternWhitespace);
            res = Regex.Replace(res, @"\{(.|\s)*\}", new MatchEvaluator(ef.CollectionFormatter), RegexOptions.IgnorePatternWhitespace);
            res = Regex.Replace(res, @"\[(.*?)\]", new MatchEvaluator(ef.FieldFormatter), RegexOptions.IgnorePatternWhitespace);
            return res;
         }

        public string IsVisibleFormatter(Match m)
        {
            var pattern = m.Value;
            var field = m.Groups[1].Value;
            try
            {
                var val = type.GetProperty(field).GetValue(data);
                if (val == null || string.IsNullOrEmpty(val.ToString()))
                    return "style='display:none'";
                return "";
            }
            catch
            {

            }
            return pattern;
        }


        public string CollectionFormatter(Match m)
        {
            var pattern = m.Value.Substring(1,m.Value.Length-2);

            var w = new StringWriter();
            var fields = Regex.Matches(pattern, @"\[(.*?)\]").Cast<Match>().Select(x => x.Groups[1].Value).ToList();
            var collectionField = fields[0].Split('.')[0];
            var val = type.GetProperty(collectionField).GetValue(data);
            foreach (var i in (IEnumerable)val)
            {
                var t = pattern;
                foreach (var f in fields)
                {
                    var type1 = i.GetType();
                    var prop = type1.GetProperty(f.Split('.')[1]);
                    if (prop != null)
                    {
                        var v = prop.GetValue(i);
                        t = t.Replace("[" + f + "]", v.ToString());
                    }
                }
                w.WriteLine(t);
            }

            var s = w.ToString();
            return s;

        }

        public string FieldFormatter(Match m)
        {
            var field = m.Groups[1].Value;
            try
            {
                var val = type.GetProperty(field).GetValue(data);
                return val.ToString();
            }
            catch
            {
                //email template contains invalid field
            }
            return m.Value;
        }
    }
}