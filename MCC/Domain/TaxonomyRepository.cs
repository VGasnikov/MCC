using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class TaxonomyRepository
    {
        public static List<Taxonomy> GetTaxonomiesByIds(List<Guid> ids)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper();
            var l = new List<Taxonomy>();
            var da = new SqlDataAdapter("api_GetTaxonomiesByIds", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Ids", ids.ToIdTable());
            da.SelectCommand.Parameters.AddWithValue("@Language", lang);
            var dt = new DataTable();
            da.Fill(dt);                       
            var hotelFacilities = dt.AsEnumerable().Select(x => new Taxonomy {Id =(Guid)x["TaxonomyId"], Title = (string)x["Name"], ParentTaxonomyId = x["ParentTaxonomyId"].ToString()}).OrderBy(x=>x.Title);
            l.AddRange(hotelFacilities);
            return l;
        }
    }
}