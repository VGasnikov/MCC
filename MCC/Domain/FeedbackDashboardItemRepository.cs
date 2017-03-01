using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


namespace MCC.Domain
{

    public class FeedbackDashboardItemRepository 
    {

        public static DataTable GetFeedbacks(Guid userId)
        {
            //var userId = new Guid("65490C40-7C91-4C92-A939-E3A8945238C5");
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var da = new SqlDataAdapter("dbo.api_GetFeedbackList", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Language", lang);
            da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
            var dt = new DataTable();
            da.Fill(dt);
            foreach (DataColumn col in dt.Columns)
                col.Caption = TranslationRepository.GetLabel("Dashboard", col.ColumnName);
            return dt;
        }
        public static List<FeedbackDashboardItem> GetUserFeedbacks(Guid userId)
        {
            var dt=GetFeedbacks(userId);
            return GetFeedbackList(dt);
        }

        private static List<FeedbackDashboardItem> GetFeedbackList(DataTable dt)
        {
            var l = new List<FeedbackDashboardItem>();

            foreach (DataRow r in dt.Rows)
            {
                var o = new FeedbackDashboardItem();
                o.Id = (int)r["FeedbackNumber"];
                o.FeedbackType = r["FeedbackType"].ToString();
                o.EntryDate = ((DateTime)r["FeedbackDate"]).ToLocalTime();
                o.HotelTransportationName = r["HotelTransportationName"].ToString();
                o.Topic= r["FeedbackTopic"].ToString();
                o.City= r["City"].ToString();
                o.Name= r["Name"].ToString();
                o.Status= r["FeedbackStatus"].ToString();
                o.ResolutionDate= r["ResolutionDate"].ToString();
                o.Priority = r["Priority"].ToString();
                l.Add(o);
            }
            return l;
        }
    }
}




