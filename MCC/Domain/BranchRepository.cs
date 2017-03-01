using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class BranchRepository
    {
        public static string GetHotelBranchNameById(string id) //{City}, {Title}
        {
            if (string.IsNullOrEmpty(id) || id.Length != 36)
                return id;
            var da = new SqlDataAdapter("SELECT Branch from vwBranches WHERE BranchId=@BranchId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@BranchId", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return id;
            return dt.Rows[0]["Branch"].ToString();
        }
        public static Dictionary<Guid, string> GetBranchesByAirline(Guid airlineId)
        {
            var da = new SqlDataAdapter("api_GetBranchesByAirlineId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => (Guid)x["BranchId"], x => x["Branch"].ToString());
        }
    }
}