using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class AudienceRepository
    {
        public static List<string> GetAudiences()
        {
            var sql = "SELECT SystemName FROM cpsys_Audiences";
            var da = new SqlDataAdapter(sql, MvcApplication.cnStr);
            var dt = new DataTable();
            da.Fill(dt);
            var l = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                l.Add(r[0].ToString());
            }
            return l;
        }
    }
}