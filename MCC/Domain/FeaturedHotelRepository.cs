using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class FeaturedHotelRepository
    {
        public static List<FeaturedHotel> GetFeaturedHotels(List<Guid> featuredHotelIds)
        {
            if (featuredHotelIds.Count == 0)
                return new List<FeaturedHotel>();

            var ids = string.Join(",", featuredHotelIds.Select(x => string.Format("'{0}'", x)).ToArray());
            var sql = "SELECT AutoNumber, Title, Phone, Logo01, Address01, City, State, PostalCode  FROM vwFeaturedHotels WHERE HotelId IN (" + ids + ")";
            var cnStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            var da = new SqlDataAdapter(sql, cnStr);
            var dt = new DataTable();
            da.Fill(dt);
            return GetFeaturedHotels(dt);
        }

        public static List<FeaturedHotel> GetFeaturedHotels(Guid airlineId, string airportCode)
        {
            var da = new SqlDataAdapter("api_GetFeaturedHotels", MvcApplication.cnStr);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@AirportCode", airportCode);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            var dt = new DataTable();
            da.Fill(dt);
            return GetFeaturedHotels(dt);
        }

        private static List<FeaturedHotel> GetFeaturedHotels(DataTable dt)
        {
            var l = new List<FeaturedHotel>();
            foreach (DataRow r in dt.Rows)
            {
                var hotel = new FeaturedHotel();
                hotel.Id = (int)r["AutoNumber"];
                hotel.Name = r["Title"].ToString();
                hotel.Phone = r["Phone"].ToString();
                hotel.Logo01 = r["Logo01"].ToString();
                hotel.Address01 = r["Address01"].ToString();
                hotel.City = r["City"].ToString();
                hotel.State = r["State"].ToString();
                hotel.PostalCode = r["PostalCode"].ToString();
                l.Add(hotel);
            }
            return l;
        }
    }
}