using System.Collections.Generic;
using System.Linq;
using System;

namespace MCC.Domain
{
    public class Hotel
    {
        private readonly AreaInformationRepository _hotelRestaurantRepository;
        public Hotel()
        {
            _hotelRestaurantRepository = new AreaInformationRepository();
        }
        public Guid IdGuid { get; set; }
        public int Id { get; set; }
        public string IntegrationId { get; set; }
        public string Name { get; set; }
        public string FeaturedType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Logo01 { get; set; }
        public string Logo02 { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }

        public string Address { get
            {
                return Address01 + (string.IsNullOrEmpty(Address02) ? "" : ", " + Address02);
            }
        }
        public IList<string> Images { get; set; }

        public string RoomAccess { get; set; }
        public string NumberOfFloors { get; set; }
        public string NumberOfRooms { get; set; }
        public string NumberOfLifts { get; set; }
        public string YearBuilt { get; set; }
        public string LastRenovationYear { get; set; }
        public string NumberOfRoomsRenovated { get; set; }
        public string PublicSpaceRenovated { get; set; }
        public string PropertyType { get; set; }
        public string MarketTier { get; set; }

        public List<Taxonomy> Facilities { get; set; }
        public List<AreaInformation> AreaInformation { get; set; }
        public CrewTransportation CrewTransportation { get; set; }
        public CrewBenefits CrewBenefits { get; set; }
    }
}