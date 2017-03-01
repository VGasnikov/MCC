using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class AirlineRepository
    {
        public static Guid? GetAirlineIdByAudienceName(string audienceName)
        {
            var da = new SqlDataAdapter("SELECT AirlineId FROM vwAirlineAudiences WHERE AudienceName=@AudienceName", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AudienceName", audienceName);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
                return (Guid)dt.Rows[0]["AirlineId"];
            return null;
        }
        public static Airline GetAirlineById(Guid id)
        {
            Airline airline = null;
            var da = new SqlDataAdapter("SELECT * from vwAirlines WHERE AirlineId=@Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                var r = dt.Rows[0];
                airline = new Airline()
                {
                    Id = (Guid)r["AirlineId"],
                    Name = r["Airline"].ToString(),
                    DisplayBranchInFeedback = (bool)r["DisplayBranchInFeedback"],
                    FeaturedHotelIds = new List<Guid>() {
                    (Guid)r["FeaturedHotels1"],
                    (Guid)r["FeaturedHotels2"],
                    (Guid)r["FeaturedHotels3"],
                    (Guid)r["FeaturedHotels4"]
                    },
                    FeaturedAirportIds = new List<Guid>() {
                    (Guid)r["DashboardCity1"],
                    (Guid)r["DashboardCity2"],
                    (Guid)r["DashboardCity3"],
                    (Guid)r["DashboardCity4"]
                    }
                };
            }          
            return airline;
        }

        private Dictionary<string, string> GetAirlnes(Guid userId)
        {
            var sql = @"
  IF EXISTS (SELECT * FROM vwUsersRoles ur 
			 INNER JOIN vwRoles r ON r.RoleId = ur.RoleId
			 WHERE r.SystemName = 'InternalAdmin' AND ur.UserId = @UserId)
	SELECT AirlineId, Airline FROM vwAirlines ORDER BY Airline
  ELSE
	SELECT AirlineId, Airline FROM dbo.fnGetUserAirlines(@UserId)
";
            var da = new SqlDataAdapter(sql, MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@UserId", userId);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => x[0].ToString(), x => x[1].ToString());
        }
    }
}