using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class AirportRepository
    {
        public static List<Airport> GetAirports(List<Guid> airportIds)
        {
            if (airportIds.Count == 0)
                return new List<Airport>();

            var ids = string.Join(",", airportIds.Select(x => string.Format("'{0}'", x)).ToArray());
            var sql = "SELECT * FROM vwAirports WHERE AirportId IN (" + ids + ")";
            var da = new SqlDataAdapter(sql, MvcApplication.cnStr);
            var dt = new DataTable();
            da.Fill(dt);
            return GetAirports(dt);
        }

        private static List<Airport> GetAirports(DataTable dt)
        {
            var l = new List<Airport>();
            foreach (DataRow r in dt.Rows)
            {
                var a = new Airport();
                a.AirportId = (Guid)r["AirportId"];
                a.AirportCode = r["AirportCode"].ToString();
                a.AirportFullName = r["AirportFullName"].ToString();
                a.CountryCode = r["CountryCode"].ToString();
                a.City = r["City"].ToString();
                l.Add(a);
            }
            return l;
        }
    }
}