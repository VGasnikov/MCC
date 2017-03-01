using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace MCC.Domain
{
    public class FeedbackTopicRepository
    {
        public static FeedbackTopic GetFeedbackTopicById(Guid id)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            var isDefaultLanguage = lang.Equals("EN");
            var FeedbackTopic = isDefaultLanguage ? "FeedbackTopic" : ("ISNULL([FeedbackTopic-" + lang+"], FeedbackTopic) AS FeedbackTopic");
            var sql = string.Format(@"SELECT {0}, FeedbackTopicId, AutoNumber, Category, [Group], [Priority] 
                        FROM vwFeedbackTopics WHERE FeedbackTopicId=@Id",FeedbackTopic);
            var da = new SqlDataAdapter(sql, MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count == 0 ? null : GetFeedbackTopic(dt.Rows[0]);
        }

        public static List<FeedbackTopic> GetFeedbackTopicByCategory(FeedbackCategory category, Guid airlineId)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            var isDefaultLanguage = lang.Equals("EN");
            var topic = isDefaultLanguage ? "FeedbackTopic" : ("ISNULL([FeedbackTopic-" + lang+"], FeedbackTopic) AS FeedbackTopic");

            var sql = string.Format(@"SELECT {0}, FeedbackTopicId, AutoNumber, Category, [Group], [Priority] FROM vwFeedbackTopics f WHERE Category ",topic);
            switch(category)
            {
                case FeedbackCategory.Hotel:
                    sql += "IN(0,2)";
                    break;
                case FeedbackCategory.Transport:
                    sql += "IN(1,2)";
                    break;
            }
            sql += " AND FeedbackTopicId <> '" + FeedbackTopic.Compliment + "'";
            sql +=" AND EXISTS(SELECT * FROM vwFeedbackTopicsAirlines fa WHERE fa.FeedbackTopicId = f.FeedbackTopicId AND fa.AirlineId = @AirlineId) ORDER BY 1";

            var da = new SqlDataAdapter(sql, MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            var dt = new DataTable();
            da.Fill(dt);
            var l = new List<FeedbackTopic>();
            foreach (DataRow r in dt.Rows)
                l.Add(GetFeedbackTopic(r));
            return l;
        }
        private static FeedbackTopic GetFeedbackTopic(DataRow r)
        {
            var priority = FeedbackPriority.Low;
            Enum.TryParse(r["Priority"].ToString(), out priority);

            return new FeedbackTopic
            {
                AutoNumber = (int)r["AutoNumber"],
                Category = (FeedbackCategory)r["Category"],
                Name = r["FeedbackTopic"].ToString(),
                FeedbackTopicId = (Guid)r["FeedbackTopicId"],
                Group = r["Group"].ToString(),
                Priority = priority
            };
        }

    }
}