using System.Collections.Generic;
using MCC.Domain;

namespace MCC.Models
{
    public class FeaturedHotelsModel
    {
        public List<Airport> Airports { get; set; }
        public List<FeaturedHotel> Hotels { get; set; }
    }
}