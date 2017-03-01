using System;
namespace MCC.Domain
{
    public class Airport
    {
      public Guid AirportId{ get; set; }
      public string AirportCode{ get; set; }
      public string AirportName{ get; set; }
      public string City{ get; set; }
      public string CountryCode{ get; set; }
      public string AirportFullName{ get; set; }
    }
}