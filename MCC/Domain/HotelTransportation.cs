using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCC.Domain
{
    public class HotelTransportation
    {
      public Guid TransportationId { get; set; }
      public string Title { get; set; }
      public string AllowComments { get; set; }
      public string Audiences { get; set; }
      public string IntegrationID { get; set; }
      public string HotelToAirportMiles { get; set; }
      public string HotelToAirportKms { get; set; }
      public string HotelToAirportMinsAway { get; set; }
      public string HotelToDowntownMiles { get; set; }
      public string HotelToDowntownKms { get; set; }
      public string HotelToDowntownMinsAway { get; set; }
      public string HotelToRestaurantsMiles { get; set; }
      public string HotelToRestaurantsKms { get; set; }
      public string HotelToRestaurantsMinsAway { get; set; }
      public string HotelToMovieTheaterMiles { get; set; }
      public string HotelToMovieTheaterKms { get; set; }
      public string HotelToMovieTheaterMinsAway { get; set; }
      public string HotelToShoppingCenterMiles { get; set; }
      public string HotelToShoppingCenterKms { get; set; }
      public string HotelToShoppingCenterMinsAway { get; set; }
      public string HotelToPharmacyMiles { get; set; }
      public string HotelToPharmacyKms { get; set; }
      public string HotelToPharmacyMinsAway { get; set; }
      public string HotelToOtherMiles { get; set; }
      public string HotelToOtherKms { get; set; }
      public string HotelToOtherMinsAway { get; set; }
      public string DistanceToMetroSubwayMiles { get; set; }
      public string DistanceToMetroSubwayKms { get; set; }
      public string DistanceToMetroSubwayMinsAway { get; set; }
      public string DistanceToTrainMiles { get; set; }
      public string DistanceToTrainKms { get; set; }
      public string DistanceToTrainMinsAway { get; set; }
      public string DistanceToBusStopMiles { get; set; }
      public string DistanceToBusStopKms { get; set; }
      public string DistanceToBusStopMinsAway { get; set; }
      public bool HotelToAirportComplimentary { get; set; }
      public bool HotelToDowntownComplimentary { get; set; }
      public bool HotelToRestaurantsComplimentary { get; set; }
      public bool HotelToMovieTheaterComplimentary { get; set; }
      public bool HotelToShoppingCenterComplimentary { get; set; }
      public bool HotelToPharmacyComplimentary { get; set; }
      public bool HotelToOtherComplimentary { get; set; }
      public bool DistanceToMetroSubwayComplimentary { get; set; }
      public bool DistanceToTrainComplimentary { get; set; }
      public bool DistanceToBusStopComplimentary { get; set; }
    }
}