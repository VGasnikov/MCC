using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class UserRepository
    {
        public static string GetUserNameById(Guid id, string format = null)
        {
            var da = new SqlDataAdapter("SELECT UserName from vwUsers WHERE UserId=@UserId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@UserId", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return "";
            var r = dt.Rows[0];
            if (string.IsNullOrEmpty(format))
                return r["UserName"].ToString();

            return r.Format(format);
        }

        public static User GetUserById(Guid id)
        {
            var da = new SqlDataAdapter("SELECT * from vwUsers WHERE UserId=@UserId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@UserId", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return null;
            return GetUser(dt.Rows[0]);
        }

        public static List<User> GetAirlineUsers(Guid airlineId, string searchStr)
        {
            var userId = "1B2C218A-6077-4CDA-B8E4-066D80DAFC4E";// APIAdmin    System.Web.HttpContext.Current.User.Identity.Name;
            var da = new SqlDataAdapter("api_GetAirlineUsers", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            da.SelectCommand.Parameters.AddWithValue("@Filter", searchStr);
            da.SelectCommand.Parameters.AddWithValue("@CurrentUserId", userId);
            var dt = new DataTable();
            da.Fill(dt);
            var l = new List<User>();
            foreach (DataRow r in dt.Rows)
                l.Add(GetUser(r));
            return l;
        }

        private static User GetUser(DataRow r)
        {
            var o = new User();
            o.Id = (Guid)r["UserId"];
            o.UserName = r["UserName"].ToString();
            o.FullName = r["FullName"].ToString();
            o.FirstName = r["FirstName"].ToString();
            o.LastName = r["LastName"].ToString();
            o.Email = r["Email"].ToString();
            o.EmployeeId = r["EmployeeId"].ToString();
            if (r["AirlineId"] != DBNull.Value)
                o.AirlineId = (Guid)r["AirlineId"];
            return o;
        }
    }
}