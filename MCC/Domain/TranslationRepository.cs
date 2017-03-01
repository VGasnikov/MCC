using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace MCC.Domain
{
    public class TranslationRepository
    {
        private static Hashtable Translations = new Hashtable();
        public static void Save(Translation model)
        {
            var key = String.Format("{0};{1};{2}", model.Language, model.Page, model.Field);
            Translations[key] = model.Value;

            var isDefaultLanguage = model.Language.Equals("en");
            var val = isDefaultLanguage?"[Value]":("Value"+model.Language);

            if (isDefaultLanguage && string.IsNullOrEmpty(model.Value))
                return;

            var sql = @"IF NOT EXISTS(SELECT * FROM Translations WHERE Field = @Field AND Page = @Page)
                            INSERT INTO Translations (Page,Field,Value" + (isDefaultLanguage ? "" : "," + val)
                            + @") VALUES (@Page,@Field,@Value" + (isDefaultLanguage ? "" : ",@ValueLocalized") + @")
                       ELSE
                            UPDATE Translations SET " + val + " = " + (isDefaultLanguage ? "@Value" : "@ValueLocalized") + " WHERE Field = @Field AND Page = @Page AND ISNULL("+val+",'')<>"+ (isDefaultLanguage ? "@Value" : "@ValueLocalized");

            using (var cn = new SqlConnection(MvcApplication.cnMCC))
            {
                cn.Open();
                var cm = new SqlCommand(sql, cn);
                cm.Parameters.AddWithValue("@Page", model.Page);
                cm.Parameters.AddWithValue("@Field", model.Field);
                if (isDefaultLanguage)
                {
                    if (string.IsNullOrEmpty(model.Value))
                        cm.Parameters.AddWithValue("@Value", model.Field.SplitCapitalsInPlace());
                    else
                        cm.Parameters.AddWithValue("@Value", model.Value.Trim());
                }
                else
                {
                    cm.Parameters.AddWithValue("@Value", model.Field.SplitCapitalsInPlace());
                    if (string.IsNullOrEmpty(model.Value))
                        cm.Parameters.AddWithValue("@ValueLocalized", DBNull.Value);
                    else
                        cm.Parameters.AddWithValue("@ValueLocalized", model.Value.Trim());

                }
                cm.ExecuteNonQuery();
            }
        }


        private static string GetSqlField(string language)
        {
            if(language.Equals("en"))
                return "[Value]";
            return string.Format("COALESCE(Value{0}, [Value])",language);           
        }

        public static string GetLabel(string page, string field)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            return GetLabel(page, field, lang);
        }
        public static string GetLabel(string page, string field, string language)
        {
            var valField = GetSqlField(language);
            var isDefaultLanguage = valField.Equals("[Value]");

            var key = String.Format("{0};{1};{2}", language, page, field);
            if (Translations.ContainsKey(key))
                return (string)Translations[key];

            var sql = "SELECT " + valField + @" FROM dbo.Translations WHERE [Page]=@Page AND Field=@Field
                        IF @@ROWCOUNT = 0
                        INSERT INTO dbo.Translations ([Page],Field,Value) VALUES (@Page, @Field, @Value)";

            var da = new SqlDataAdapter(sql, MvcApplication.cnMCC);
            da.SelectCommand.Parameters.AddWithValue("@Page", page);
            da.SelectCommand.Parameters.AddWithValue("@Field", field);
            da.SelectCommand.Parameters.AddWithValue("@Value", field.SplitCapitalsInPlace());
            var dt = new DataTable();
            da.Fill(dt);
            var txt = dt.Rows.Count == 1 ? (string)dt.Rows[0][0] : field.SplitCapitalsInPlace();         
            Translations.Add(key,txt);
            return txt;
        }
    }
}