using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class AreaInformationRepository
    {
        public static List<AreaInformation> GetAreaInformation(string integrationId)
        {
            var da = new SqlDataAdapter("SELECT TOP 10 * FROM vwAreaInformation WHERE IntegrationId = @Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", integrationId);
            var dt = new DataTable();
            da.Fill(dt);
            return GetAreaInformation(dt);
        }
        public static List<AreaInformation> GetAreaInformation(DataTable dt)
        {
            var l = new List<AreaInformation>();

            foreach(DataRow r in dt.Rows)
            {
                var o = new AreaInformation();
                o.Id = (Guid)r["RestaurantId"];
                o.Title = r["Title"].ToString();
                o.AutoNumber = (int)r["Autonumber"];
                o.IntegrationId = r["IntegrationId"].ToString();
                o.TaxonomyId = r["TaxonomyId"].ToString();
                o.Phone = r["Phone"].ToString();
                o.TollFreeNumber = r["TollFreeNumber"].ToString();
                o.InternationalPhoneNumber = r["InternationalPhoneNumber"].ToString();
                o.Fax = r["Fax"].ToString();
                o.ContactName = r["ContactName"].ToString();
                o.DistanceInMiles = r["DistanceInMiles"].ToString();
                o.DistanceInKm = r["DistanceInKm"].ToString();
                o.StartTime = r["StartTime"].ToString();
                o.EndTime = r["EndTime"].ToString();
                o.StartTime02 = r["StartTime02"].ToString();
                o.EndTime02 = r["EndTime02"].ToString();
                o.DiscountPercentage = r["DiscountPercentage"].ToString();
                o.HotelDelivery = r["HotelDelivery"].ToString();
                o.Website = r["Website"].ToString();
                o.Address01 = r["Address01"].ToString();
                o.Address02 = r["Address02"].ToString();
                o.Hours24 = (bool)r["Hours24"];
                l.Add(o);
            }
            return l;
        }
    }
}