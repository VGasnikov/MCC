using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class FeedbackCommentRepository
    {
        public static List<FeedbackComment> GetFeedbackComments(Guid feedbackId)
        {
            var da = new SqlDataAdapter("SELECT * FROM vwFeedbackComments WHERE FeedbackId=@FeedbackId ORDER BY DateCreated DESC", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@FeedbackId", feedbackId.ToString());
            var dt = new DataTable();
            da.Fill(dt);
            return GetFeedbackComments(dt);
        }
        private static List<FeedbackComment> GetFeedbackComments(DataTable dt)
        {
            var l = new List<FeedbackComment>();
            foreach (DataRow r in dt.Rows)
            {
                var feedbackStatus = FeedbackStatus.Open;
                Enum.TryParse(r["FeedbackStatus"].ToString(), out feedbackStatus);

                l.Add(new FeedbackComment{
                    FeedbackCommentId = (Guid)r["FeedbackCommentId"],
                    FeedbackId = Guid.Parse(r["FeedbackId"].ToString()),
                    DateCreated = (DateTime)r["DateCreated"],
                    FeedbackStatus = feedbackStatus,
                    ResponseComment = r["ResponseComment"].ToString(),
                    UserName = r["UserName"].ToString()
                });
            }
            return l;
        }

        public static void AddFeedbackComment(FeedbackComment comment)
        {
            using (var cn = new SqlConnection(MvcApplication.cnStr))
            {
                var cm = new SqlCommand("api_AddFeedbackComment", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@FeedbackId", comment.FeedbackId);
                cm.Parameters.AddWithValue("@Comments", comment.ResponseComment ?? "");
                cm.Parameters.AddWithValue("@FeedbackStatusId", (int)comment.FeedbackStatus);
                cm.Parameters.AddWithValue("@UserName", comment.UserName);
                cn.Open();
                cm.ExecuteNonQuery();
            }
        }
    }
}