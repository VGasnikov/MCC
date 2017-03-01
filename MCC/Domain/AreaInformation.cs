using System;

namespace MCC.Domain
{
    public class AreaInformation
    {
        public Guid Id { get; set; }
        public int AutoNumber { get; set; }
        public string Title { get; set; }
        public string IntegrationId { get; set; }
        public string TaxonomyId { get; set; }
        public string Phone { get; set; }
        public string TollFreeNumber { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string Fax { get; set; }
        public string ContactName { get; set; }
        public string DistanceInMiles { get; set; }
        public string DistanceInKm { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartTime02 { get; set; }
        public string EndTime02 { get; set; }
        public string DiscountPercentage { get; set; }
        public string HotelDelivery { get; set; }
        public string Website { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public bool Hours24 { get; set; }

    }
}