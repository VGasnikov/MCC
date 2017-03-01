using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MCC.Models
{
    public class SearchUsersModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? UserStatus { get; set; }
        public Guid? Airline { get; set; }
        
        public class User
        {
            [JsonProperty(PropertyName = "a")]
            public string UserId { get; set; }

            [JsonProperty(PropertyName = "b")]
            public string UserName { get; set; }

            [JsonProperty(PropertyName = "c")]
            public string FirstName { get; set; }

            [JsonProperty(PropertyName = "d")]
            public string LastName { get; set; }

            [JsonProperty(PropertyName = "e")]
            public string Roles { get; set; }

            [JsonProperty(PropertyName = "f")]
            public string Status { get; set; }

            [JsonProperty(PropertyName = "g")]
            public string Email { get; set; }
        }
        public List<SearchUsersModel.User> GetUsers()
        {
            var l = new List<SearchUsersModel.User>();
            if (!UserStatus.HasValue)
                return l;
            var userId = new Guid("5734B3DE-A11D-4312-8870-B4FFA230FE8F");//jbladmin
            var language = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var da = new SqlDataAdapter("api_UsersSearch", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
            da.SelectCommand.Parameters.AddWithValue("@Language", language);
            da.SelectCommand.Parameters.AddWithValue("@UserName", UserName);
            if (!Airline.HasValue)
                da.SelectCommand.Parameters.AddWithValue("@Airline", DBNull.Value);
            else
                da.SelectCommand.Parameters.AddWithValue("@Airline", Airline.Value);
            da.SelectCommand.Parameters.AddWithValue("@Email", Email);
            da.SelectCommand.Parameters.AddWithValue("@UserStatus", UserStatus);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow r in dt.Rows)
            {
                l.Add(new SearchUsersModel.User
                {
                    FirstName = r["FirstName"].ToString(),
                    LastName = r["LastName"].ToString(),
                    Roles = r["Roles"].ToString(),
                    UserId = r["UserId"].ToString(),
                    UserName = r["UserName"].ToString(),
                    Status = r["Status"].ToString(),
                    Email = r["Email"].ToString()
                });
            }
            return l;
        }
    }
}