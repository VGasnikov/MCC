using MCC.Domain;

namespace MCC.Models
{
    public class HotelInfoModel
    {
        public Hotel Hotel { get; set; }
        public HotelTransportation Transportation { get; set; }


        public HotelInfoModel(int id)
        {
            Hotel = HotelRepository.GetHotelById(id);
            Transportation = HotelTransportationRepository.GetHotelByIntegrationId(Hotel.IntegrationId);

        }
    }
}