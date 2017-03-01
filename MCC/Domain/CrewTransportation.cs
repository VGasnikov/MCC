namespace MCC.Domain
{
    public class CrewTransportation
    {
        public string IntegrationId { get; set; }
        public string Details { get; set; }
        public string ShuttleHoursOfOperationFrom { get; set; }
        public string ShuttleHoursOfOperationUntil { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public bool CrewTransportIsConductedByHotel { get; set; }
        public bool TransportIsConductedByOutsideService { get; set; }
        public bool Shuttle24HoursOfOperation { get; set; }
        public bool CarRentalAtHotel { get; set; }
        public string GroundTransportTime { get; set; }
        public string TaxiCarServiceCompany { get; set; }
        public string TaxiCarServiceCompanyPhone { get; set; }
        public string CarRentalCompany { get; set; }
        public string CarRentalCompanyPhone { get; set; }
    }
}