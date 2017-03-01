using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class EmailTemplateRepository
    {
        public static EmailTemplate GetTemplate(int id, string language)
        {
            var sql = @"SELECT d.Subject, d.Template, Class 
                        FROM EmailTemplates t 
                        LEFT JOIN EmailTemplateDetails d ON t.Id = d.EmailTemplateId AND @Language = d.Language 
                        WHERE t.Id = @Id";
            var da = new SqlDataAdapter(sql, MvcApplication.cnMCC);
            da.SelectCommand.Parameters.AddWithValue("@Language", language??"EN");
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);
            var t = new EmailTemplate { EmailTemplateId = id, Language = language };
            if (dt.Rows.Count > 0)
            {
                t.Template = dt.Rows[0]["Template"].ToString();
                t.Subject = dt.Rows[0]["Subject"].ToString();
                t.Class = dt.Rows[0]["Class"].ToString();
            }
            return t;
        }
        public static Dictionary<int,string> GetTemplates()
        {
            var da = new SqlDataAdapter("SELECT Id, Name FROM EmailTemplates ORDER BY Name", MvcApplication.cnMCC);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => (int)x[0], x => x[1].ToString());
        }
        public static void Save(int id, string language, string emailTemplate, string subject)
        {
            using (var cn = new SqlConnection(MvcApplication.cnMCC))
            {
                var sql = @"IF EXISTS (SELECT * FROM EmailTemplateDetails WHERE @Language = Language AND EmailTemplateId = @Id)
                                UPDATE EmailTemplateDetails SET Template = @Template, Subject = @Subject WHERE @Language = Language AND EmailTemplateId = @Id
                            ELSE 
                                INSERT INTO EmailTemplateDetails(EmailTemplateId, Language, Template, Subject) VALUES (@Id, @Language, @Template, @Subject)";


                var cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Template", emailTemplate.Trim());
                cm.Parameters.AddWithValue("@Language", language);
                cm.Parameters.AddWithValue("@Subject", subject);
                cm.Parameters.AddWithValue("@Id", id);
                cn.Open();
                cm.ExecuteNonQuery();
            }
        }
    }
}