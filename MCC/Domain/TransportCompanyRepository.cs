using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class TransportCompanyRepository
    {
        public static string GetTransportCompanyNameById(Guid id, string format = null)
        {
            var da = new SqlDataAdapter("SELECT * from vwTransportCompanies WHERE TransportCompanyId = @TransportCompanyId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@TransportCompanyId", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return "";
            var r = dt.Rows[0];
            if (string.IsNullOrEmpty(format))
                return r["TransportCompany"].ToString();

            return r.Format(format);
        }

        public static Dictionary<Guid, string> TransportCompaniesByAirline(Guid airlineId)
        {
            var da = new SqlDataAdapter("api_GetTransportCompaniesByAirlineId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => (Guid)x["TransportCompanyId"], x => x["TransportCompany"].ToString());
        }
    }
}