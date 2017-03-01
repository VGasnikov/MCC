using System.Collections.Generic;
using System;

namespace MCC.Domain
{
    public class Airline
    {


        public const string VirginAustralia = "7C35D893-0207-4C9D-B0BF-FF3CA9025559";
        public const string AirNewZealand = "03EBCE01-7E7E-4CD1-9160-995167ED6CA7";
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> FeaturedHotelIds { get; set; }
        public List<Guid> FeaturedAirportIds { get; set; }
        public bool DisplayBranchInFeedback { get; set; }
    }
}