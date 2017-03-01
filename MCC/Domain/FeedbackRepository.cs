using System.Data.SqlClient;
using System.Data;
using System;

namespace MCC.Domain
{
    public class FeedbackRepository
    {
        public static Feedback GetFeedbackById(int id)
        {
            var da = new SqlDataAdapter("SELECT top 1 * FROM vwFeedbacks WHERE FeedbackNumber=@Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count == 0 ? null : GetFeedback(dt.Rows[0]);
        }
        private static Feedback GetFeedback(DataRow r)
        {
            var o = new Feedback();
            o.FeedbackId = (Guid)r["FeedbackId"];
            o.FeedbackNumber = (int)r["FeedbackNumber"];
            o.FeedbackDate = (DateTime)r["FeedbackDate"];
            o.ModifyDate = (DateTime)r["ModifyDate"];
            o.Title = r["Title"].ToString();
            o.CheckIn = (DateTime)r["CheckInDate"];
            o.CheckOut = (DateTime)r["CheckOutDate"];

            var feedbackType = FeedbackType.Complaint;
            Enum.TryParse(r["FeedbackType"].ToString(),out feedbackType);
            o.Type = feedbackType;

            o.Priority = (FeedbackPriority)r["FeedbackPriorityId"];

            var feedbackStatus = FeedbackStatus.Open;
            Enum.TryParse(r["FeedbackStatus"].ToString(),out feedbackStatus);
            o.Status = feedbackStatus;

            var feedbackCategory = FeedbackCategory.Hotel;
            Enum.TryParse(r["FeedbackHotelTransport"].ToString(),out feedbackCategory);
            o.Category = feedbackCategory;

            o.ResourceGroup = r["ResourceGroup"].ToString();
            o.UserId = (Guid)r["UserId"];
            if(r["HotelId"] !=DBNull.Value)
                o.HotelId = (Guid)r["HotelId"];
            if(r["TransportCompanyId"] !=DBNull.Value)
                o.TransportCompanyId = (Guid)r["TransportCompanyId"];
            o.Subject = r["Subject"].ToString();
            o.Comments = r["Comments"].ToString();
            o.RoomNumber = r["RoomNumber"].ToString();
            o.ArrivalFlightNumber = r["ArrivalFlightNumber"].ToString();
            o.DepartureFlightNumber = r["DepartureFlightNumber"].ToString();
            o.FirstName = r["FirstName"].ToString();
            o.LastName = r["LastName"].ToString();
            o.BranchId = r["BranchCode"].ToString();
            o.ImageUpload = r["ImageUpload"].ToString();
            o.HotelAttention = (bool)r["HotelAttention"];
            o.FeedbackTopicId = (Guid)r["FeedbackTopicId"];
            o.AirlineId = (Guid)r["AirlineId"];
            return o;

        }

        public static bool Exists(Guid feedbackId)
        {
            var da = new SqlDataAdapter("IF EXISTS(SELECT * FROM vwFeedbacks WHERE FeedbackId=@Id) SELECT CAST(1 AS BIT) ELSE SELECT CAST(0 AS BIT)", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", feedbackId);
            var dt = new DataTable();
            da.Fill(dt);
            return (bool)dt.Rows[0][0];
        }       

        public static void SaveFeedback(Feedback feedback, System.Web.HttpPostedFileBase image)
        {
            var ImgRootFolder = System.Configuration.ConfigurationManager.AppSettings["ImgRootFolder"];
            if (image != null && image.ContentLength>0)
            {
                string subFolder = Guid.NewGuid().ToString();
                var di = new System.IO.DirectoryInfo(System.IO.Path.Combine(ImgRootFolder, subFolder + "\\Images\\"));
                if (!di.Exists)
                    di.Create();

                string savedFileName = System.IO.Path.Combine(di.FullName, image.FileName);
                image.SaveAs(savedFileName);
                feedback.ImageUpload = "/Uploads/" + subFolder + "/Images/" + image.FileName;
            }
            else
                feedback.ImageUpload = "";

            if (feedback.Category == FeedbackCategory.Transport)
            {
                feedback.RoomNumber = "";
                feedback.HotelAttention = false;
            }
            else
            {
                feedback.TransportCompanyId = null;
            }

            var hotelName = HotelRepository.GetHotelNameById(feedback.HotelId.Value, "{City}, {Title}");
            var feedbackTopic = FeedbackTopicRepository.GetFeedbackTopicById(feedback.FeedbackTopicId);
            feedback.Title = hotelName + " - " + feedbackTopic;
            if (feedback.FeedbackTopicId == Guid.Empty)
                feedback.FeedbackTopicId = FeedbackTopic.Compliment;

            using (var cn = new SqlConnection(MvcApplication.cnStr))
            {
                var cm = new SqlCommand("api_SaveFeedback", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@ArrivalFlightNumber", feedback.ArrivalFlightNumber??"");
                cm.Parameters.AddWithValue("@BranchId", feedback.BranchId??"");
                cm.Parameters.AddWithValue("@CheckIn", feedback.CheckIn);
                cm.Parameters.AddWithValue("@CheckOut", feedback.CheckOut);
                cm.Parameters.AddWithValue("@Comments", feedback.Comments??"");
                cm.Parameters.AddWithValue("@DepartureFlightNumber", feedback.DepartureFlightNumber??"");
                cm.Parameters.AddWithValue("@FeedbackCategory", (int)feedback.Category);
                cm.Parameters.AddWithValue("@FeedbackStatus", feedback.Status.ToString());
                cm.Parameters.AddWithValue("@FeedbackTopicId", feedback.FeedbackTopicId);
                cm.Parameters.AddWithValue("@FeedbackType", feedback.Type.ToString());
                cm.Parameters.AddWithValue("@FirstName", feedback.FirstName??"");
                cm.Parameters.AddWithValue("@LastName", feedback.LastName??"");
                cm.Parameters.AddWithValue("@HotelAttention", feedback.HotelAttention);
                cm.Parameters.AddWithValue("@HotelId", feedback.HotelId);
                cm.Parameters.AddWithValue("@ImageUpload", feedback.ImageUpload??"");
                cm.Parameters.AddWithValue("@ResourceGroup", feedback.ResourceGroup??"");
                cm.Parameters.AddWithValue("@RoomNumber", feedback.RoomNumber??"");
                cm.Parameters.AddWithValue("@Subject", feedback.Subject??"");
                cm.Parameters.AddWithValue("@Title", feedback.Title);
                cm.Parameters.AddWithValue("@UserId", feedback.UserId);
                cm.Parameters.AddWithValue("@FeedbackId", feedback.FeedbackId);
                cn.Open();
                cm.ExecuteNonQuery();
            }
        }
        public static void AssociateNewHotel(Guid feedbackId, Guid newHotel, string comments, Guid userId)
        {
            using (var cn = new SqlConnection(MvcApplication.cnStr))
            {
                var cm = new SqlCommand("api_AssociateNewHotel", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@FeedbackId", feedbackId);
                cm.Parameters.AddWithValue("@Comments", comments ?? "");
                cm.Parameters.AddWithValue("@NewHotelId", newHotel);
                cm.Parameters.AddWithValue("@UserId", userId);
                cn.Open();
                cm.ExecuteNonQuery();
            }
        }
        public static void ChangeFeedbackTypeTopicPriority(Guid feedbackId, Guid feedbackTopicId, int priorityId, int feedbackTypeId, Guid userId)
        {
            using (var cn = new SqlConnection(MvcApplication.cnStr))
            {
                var cm = new SqlCommand("api_ChangeFeedbackTypeTopicPriority", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@FeedbackId", feedbackId);
                cm.Parameters.AddWithValue("@FeedbackTopicId", feedbackTopicId);
                cm.Parameters.AddWithValue("@PriorityId", priorityId);
                cm.Parameters.AddWithValue("@FeedbackTypeId", feedbackTypeId);
                cm.Parameters.AddWithValue("@UserId", userId);
                cn.Open();
                cm.ExecuteNonQuery();
            }
        }
    }
}