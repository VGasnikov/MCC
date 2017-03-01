using System;
namespace MCC.Domain
{
    public class Taxonomy
    {
        public const string Taxonomy_Pharmacies = "1169677e-5bd5-467c-bfe1-6f1522a432c2";
        public const string Taxonomy_Hotels = "5b6bac67-f76a-45c7-b718-f9b200ecb0dc";
        public const string Taxonomy_LocalRestaurants = "e3ba2162-2466-45fc-b65b-f35acde28c85";
        public const string Taxonomy_LocalHospitals = "d1b4075d-0f6c-4f1b-a90f-09896f21976d";
        public const string Taxonomy_LocalAttractions = "4e7df7e6-992f-4a57-9f81-b3c12bfa58ae";
        public const string Taxonomy_LocalInformation = "21b06d03-1ba3-46ee-9965-d8ce7ee9283d";
        public const string Taxonomy_LocalShopping = "92f6e880-d186-4920-ac60-d0a11ed3ee80";
        public const string Taxonomy_OtherRestaurants = "74e7de21-6fb9-4c65-bc95-62ebf2586f0d";
        public const string Taxonomy_RoomService = "6223b7c8-1014-4bf2-b990-d7c69500c8fa";

        public const string Taxonomy_GuestRoomAmenities = "83b4084d-d923-43f6-ba5c-b9692fcfa847";
        public const string Taxonomy_MoreHotelAmenities = "f5cbcd66-e3ee-4712-81c5-c5200000b936";
        public const string Taxonomy_BathroomAmenities = "e4dc1bff-e3e5-4d0f-97cb-1110306609df";
        public const string Taxonomy_HotelRecreationalFacilities = "f1195659-f9fe-4ddf-b914-ceb6bc93cffa";
        public const string Taxonomy_HotelFacilitiesServicesOnTheProperty = "2fb79d53-ef7b-4795-a65f-6c747d27acbe";
        public const string Taxonomy_SafetyAndSecurity = "971a71e2-5257-47fe-b92f-a411f6ebd187";
        public const string Taxonomy_Location = "0f4aa3e3-2a2c-4502-94cf-5c7c10bb18aa";
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ParentTaxonomyId { get; set; }
    }
}