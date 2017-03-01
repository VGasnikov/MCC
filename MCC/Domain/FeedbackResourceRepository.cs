using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class FeedbackResourceRepository
    {
        public static Dictionary<Guid,string> GetFeedbackResources(Guid airlineId)
        {
            var da = new SqlDataAdapter("SELECT ResourceId, ResourceName FROM vwFeedbackResources WHERE AirlineId=@Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", airlineId);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => (Guid)x[0], x => x[1].ToString());
        }
    }
}