using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;

namespace MCC.Domain
{
    public class HotelTransportationRepository
    {
        public static HotelTransportation GetHotelTransportationByIdGuid(string id)
        {
            var da = new SqlDataAdapter("SELECT * FROM vwHotelTransportations WHERE TransportationId=@Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count == 0 ? null : GetHotelTransportation(dt.Rows[0]);
        }

        public static HotelTransportation GetHotelByIntegrationId(string id)
        {
            var da = new SqlDataAdapter("SELECT * FROM vwHotelTransportations WHERE IntegrationID=@Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            var dt = new DataTable();
            da.Fill(dt);           
            return dt.Rows.Count==0?null:GetHotelTransportation(dt.Rows[0]);
        }


        private static HotelTransportation GetHotelTransportation(DataRow r)
        {
            var o = new HotelTransportation();
            o.TransportationId = (Guid)r["TransportationId"];
            o.Title = r["Title"].ToString();
            o.AllowComments = r["AllowComments"].ToString();
            o.Audiences = r["Audiences"].ToString();
            o.IntegrationID = r["IntegrationID"].ToString();
            o.HotelToAirportMiles = r["HotelToAirportMiles"].ToString();
            o.HotelToAirportKms = r["HotelToAirportKms"].ToString();
            o.HotelToAirportMinsAway = r["HotelToAirportMinsAway"].ToString();
            o.HotelToDowntownMiles = r["HotelToDowntownMiles"].ToString();
            o.HotelToDowntownKms = r["HotelToDowntownKms"].ToString();
            o.HotelToDowntownMinsAway = r["HotelToDowntownMinsAway"].ToString();
            o.HotelToRestaurantsMiles = r["HotelToRestaurantsMiles"].ToString();
            o.HotelToRestaurantsKms = r["HotelToRestaurantsKms"].ToString();
            o.HotelToRestaurantsMinsAway = r["HotelToRestaurantsMinsAway"].ToString();
            o.HotelToMovieTheaterMiles = r["HotelToMovieTheaterMiles"].ToString();
            o.HotelToMovieTheaterKms = r["HotelToMovieTheaterKms"].ToString();
            o.HotelToMovieTheaterMinsAway = r["HotelToMovieTheaterMinsAway"].ToString();
            o.HotelToShoppingCenterMiles = r["HotelToShoppingCenterMiles"].ToString();
            o.HotelToShoppingCenterKms = r["HotelToShoppingCenterKms"].ToString();
            o.HotelToShoppingCenterMinsAway = r["HotelToShoppingCenterMinsAway"].ToString();
            o.HotelToPharmacyMiles = r["HotelToPharmacyMiles"].ToString();
            o.HotelToPharmacyKms = r["HotelToPharmacyKms"].ToString();
            o.HotelToPharmacyMinsAway = r["HotelToPharmacyMinsAway"].ToString();
            o.HotelToOtherMiles = r["HotelToOtherMiles"].ToString();
            o.HotelToOtherKms = r["HotelToOtherKms"].ToString();
            o.HotelToOtherMinsAway = r["HotelToOtherMinsAway"].ToString();
            o.DistanceToMetroSubwayMiles = r["DistanceToMetroSubwayMiles"].ToString();
            o.DistanceToMetroSubwayKms = r["DistanceToMetroSubwayKms"].ToString();
            o.DistanceToMetroSubwayMinsAway = r["DistanceToMetroSubwayMinsAway"].ToString();
            o.DistanceToTrainMiles = r["DistanceToTrainMiles"].ToString();
            o.DistanceToTrainKms = r["DistanceToTrainKms"].ToString();
            o.DistanceToTrainMinsAway = r["DistanceToTrainMinsAway"].ToString();
            o.DistanceToBusStopMiles = r["DistanceToBusStopMiles"].ToString();
            o.DistanceToBusStopKms = r["DistanceToBusStopKms"].ToString();
            o.DistanceToBusStopMinsAway = r["DistanceToBusStopMinsAway"].ToString();
            o.HotelToAirportComplimentary = (bool)r["HotelToAirportComplimentary"];
            o.HotelToDowntownComplimentary = (bool)r["HotelToDowntownComplimentary"];
            o.HotelToRestaurantsComplimentary = (bool)r["HotelToRestaurantsComplimentary"];
            o.HotelToMovieTheaterComplimentary = (bool)r["HotelToMovieTheaterComplimentary"];
            o.HotelToShoppingCenterComplimentary = (bool)r["HotelToShoppingCenterComplimentary"];
            o.HotelToPharmacyComplimentary = (bool)r["HotelToPharmacyComplimentary"];
            o.HotelToOtherComplimentary = (bool)r["HotelToOtherComplimentary"];
            o.DistanceToMetroSubwayComplimentary = (bool)r["DistanceToMetroSubwayComplimentary"];
            o.DistanceToTrainComplimentary = (bool)r["DistanceToTrainComplimentary"];
            o.DistanceToBusStopComplimentary = (bool)r["DistanceToBusStopComplimentary"];
            return o;
        }
    }
}