using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MCC.Email
{
    public class EmailFeedbackResponseModel: IEmailDataModel
    {
        public class FeedbackComment
        {
            public string DateCreated { get; set; }
            public string Comment { get; set; }
            public string UserName { get; set; }
            public string FeedbackStatus { get; set; }
        }

        public string Title { get; set; }
        public int FeedbackNumber { get; set; }
        public string Airline { get; set; }
        public string RoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string ArrivalFlightNumber { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string FeedbackType { get; set; }
        public string FeedbackTopic { get; set; }
        public DateTime FeedbackDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public string Comments { get; set; }
        public string Hotel { get; set; }
        public string FeedbackStatus  { get; set; }
        public string FeedbackPriority { get; set; }
        public string Subject { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUpload { get; set; }
        public string Branch { get; set; }
        public string ResourceGroup { get; set; }
        public List<FeedbackComment> FeedbackComments { get; set; }

        public object GetObjectById(string id, string language)
        {
            int _id;
            if (!int.TryParse(id, out _id))
                return null;
            var da = new SqlDataAdapter("api_GetEmailDetails_FeedbackResponse", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FeedbackNumber", _id);
            da.SelectCommand.Parameters.AddWithValue("@Language", language);
            var ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            
            var r = ds.Tables[0].Rows[0];
            var o = new EmailFeedbackResponseModel();
            o.FeedbackNumber = (int)r["FeedbackNumber"];
            o.Airline = r["Airline"].ToString();
            o.FeedbackDate = (DateTime)r["FeedbackDate"];
            o.ModifyDate = (DateTime)r["ModifyDate"];
            o.Title = r["Title"].ToString();
            o.CheckInDate = (DateTime)r["CheckInDate"];
            o.CheckOutDate = (DateTime)r["CheckOutDate"];
            o.FeedbackType = r["FeedbackType"].ToString(); 
            o.FeedbackTopic = r["FeedbackTopic"].ToString(); 
            o.FeedbackStatus = r["FeedbackStatus"].ToString(); 
            o.FeedbackPriority = r["FeedbackPriority"].ToString(); 
            o.ResourceGroup = r["ResourceGroup"].ToString();
            o.Hotel = r["Hotel"].ToString();
            o.Subject = r["Subject"].ToString();
            o.Comments = r["Comments"].ToString();
            o.RoomNumber = r["RoomNumber"].ToString();
            o.ArrivalFlightNumber = r["ArrivalFlightNumber"].ToString();
            o.DepartureFlightNumber = r["DepartureFlightNumber"].ToString();
            o.FirstName = r["FirstName"].ToString();
            o.LastName = r["LastName"].ToString();
            o.Branch = r["Branch"].ToString();
            var img = r["ImageUpload"].ToString();
            if (!string.IsNullOrEmpty(img))
                o.ImageUpload = System.Configuration.ConfigurationManager.AppSettings["ImgRootURL"] + img;
            else
                o.ImageUpload = "";
            o.FeedbackTopic = r["FeedbackTopic"].ToString();
            o.FeedbackComments = new List<FeedbackComment>();
            foreach(DataRow dr in ds.Tables[1].Rows)
            {
                o.FeedbackComments.Add(new FeedbackComment{
                        Comment = dr["Comment"].ToString(),
                        DateCreated = dr["DateCreated"].ToString(),
                        UserName = dr["UserName"].ToString()
                });
            }

            return o;

        }

    }
}