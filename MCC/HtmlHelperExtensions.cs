using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MCC.Domain;

namespace MCC
{
    public static class HtmlHelperExtensions
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        public static string AsJson(this object src)
        {
            return JsonConvert.SerializeObject(src, Formatting.Indented, _settings);
        }

        public static MvcHtmlString Link(this HtmlHelper html, string page)
        {
            return MvcHtmlString.Create("/"+ Audience.Current + page);
        }
        public static MvcHtmlString Translate(this HtmlHelper html, string field)
        {
            return Translate(html, field, "Global");
        }
        public static MvcHtmlString Translate(this HtmlHelper html, string field, string page)
        {
            if (string.IsNullOrEmpty(field))
                field="";
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;

            var editMode = true;
            var txt=TranslationRepository.GetLabel(page, field, lang);

            if (editMode)
            {
                var builder = new TagBuilder("span");
                builder.InnerHtml = txt;
                builder.MergeAttribute("class", "apitranslate");
                builder.MergeAttribute("contenteditable", "true");
                builder.MergeAttribute("page", page);
                builder.MergeAttribute("field", field);
                builder.MergeAttribute("lang", lang);

                if (!lang.Equals("en",StringComparison.OrdinalIgnoreCase))
                {
                    var txtEn = TranslationRepository.GetLabel(page, field, "en");
                    builder.MergeAttribute("title", "In English");
                    builder.MergeAttribute("data-placement", "top");
                    builder.MergeAttribute("data-trigger", "focus");
                    builder.MergeAttribute("data-content", txtEn);
                }
                return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
            }
            return MvcHtmlString.Create(txt);
        }
        public static MvcHtmlString SelectLanguage(this HtmlHelper html, string language)
        {
            var audience = Audience.Current;
            var oldAudience = audience.ToString();
            var url = HttpContext.Current.Request.Url.ToString();
            var newUrl=url.Replace("/" + audience + "/", "/" + audience.GetLocalizedAudienceName(language) + "/");                   
            return MvcHtmlString.Create(newUrl);
        }

        private static void RenderHeaderItem(TextWriter w, string columnName)
        {
            w.Write(@"<th>                   
                        <a href='#' ng-click='sortType = 'name'; sortReverse = !sortReverse'>
                        {0}
                        <span ng-show='sortType == 'name' && !sortReverse' class='fa fa-caret-down'></span>
                        <span ng-show='sortType == 'name' && sortReverse' class='fa fa-caret-up'></span>
                    </a></th>", columnName);
        }

        public static MvcHtmlString MCCGrid(this HtmlHelper html, string name)
        {
            var userId = new Guid("65490C40-7C91-4C92-A939-E3A8945238C5");
            var cnStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            var da = new SqlDataAdapter("dbo.api_GetFeedbackList", cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
            var dt = new DataTable();
            da.Fill(dt);

            var w = new StringWriter();
            w.Write("<table class='table table-hover table-bordered table-striped'><thead><tr>");
            RenderHeaderItem(w, "Feedback ID");
            RenderHeaderItem(w, "Feedback Type");
            RenderHeaderItem(w, "Entry Date");
            RenderHeaderItem(w, "Hotel/Transportation");
            RenderHeaderItem(w, "Topic");
            RenderHeaderItem(w, "City");
            RenderHeaderItem(w, "Name");
            RenderHeaderItem(w, "Status");
            RenderHeaderItem(w, "Resolution Date");
            w.Write("</tr></thead><tbody>");
            w.Write(@"<tbody>
                        <tr ng-repeat='r in sushi | orderBy:sortType:sortReverse | filter:searchFish'>
                            <td>{{ r.AutoNumber }}</td>
                            <td>{{ r.FeedbackType }}</td>
                            <td>{{ r.FeedbackDate }}</td>
                            <td>{{ r.HotelName }}</td>
                            <td>{{ r.Topic }}</td>
                            <td>{{ r.City }}</td>
                            <td>{{ r.Name }}</td>
                            <td>{{ r.FeedbackStatus }}</td>
                            <td>{{ r.ResolutionDate }}</td>
                        </tr>
                    </tbody>");

            foreach (DataRow r in dt.Rows)
            {
                w.Write("<tr>");
                w.Write("<td class='negative'>{0}</td>",r["AutoNumber"]);
                w.Write("<td>{0}</td>",r["FeedbackType"]);
                w.Write("<td>{0}</td>", ((DateTime)r["FeedbackDate"]).ToString("MM/dd/yyyy"));
                w.Write("<td>{0}</td>", r["HotelName"]);
                w.Write("<td>{0}</td>",r["Topic"]);
                w.Write("<td class='hidden-xs'>{0}</td>",r["City"]);
                w.Write("<td class='hidden-xs'>{0}</td>",r["Name"]);
                w.Write("<td>{0}</td>",r["FeedbackStatus"]);
                w.Write("<td class='hidden-xs'>{0}</td>",r["ResolutionDate"]==DBNull.Value?"":((DateTime)r["ResolutionDate"]).ToString("MM/dd/yyyy"));
                w.Write("</tr>");  
            }
            w.Write("</tbody></table>");    
            return MvcHtmlString.Create(w.ToString());
        }

        public static MvcHtmlString DateTimePicker(this HtmlHelper html, string id, bool isRequired=true)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var str = new System.Text.StringBuilder();
            str.AppendFormat("<div class='input-group date' id='{0}'>", id);
            str.AppendFormat("<input type='text'{1} name='{0}' class='form-control'/><span class='input-group-addon'>", id, isRequired?"required":"");
            str.Append("<span class='glyphicon glyphicon-calendar'></span></span></div>");
            str.Append("<script type='text/javascript'>$(function(){");
            str.AppendFormat("$('#{0}')", id);
            str.Append(".datetimepicker({");
            str.AppendFormat("locale:'{0}'",lang);
            str.Append("}); });</script>");
            return MvcHtmlString.Create(str.ToString());
        }

    }
}