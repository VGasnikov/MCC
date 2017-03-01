using System;
using MCC.MyTravelServiceReference;

namespace MCC.Domain
{
    public class TripResv
    {
        public string CrewId { get; set; }
        public string ConfirmationNumber { get; set; }
        public ReservationStatusEnum ReservationStatus { get; set; }
        public string PairingNumber { get; set; }
        public string AirportCity { get; set; }
        public string AirportCode { get; set; }
        public DateTime TripDateUTC { get; set; }
        public DateTime TripDateLocal { get; set; }
        public DateTime CheckInDateTimeLocal { get; set; }
        public DateTime CheckOutDateTimeLocal { get; set; }
        public int TripReservationId { get; set; }
        public string HotelName { get; set; }
        public string HotelPhone { get; set; }
        public string HotelAddress { get; set; }
        public int HotelSupplierId { get; set; }

        public bool GTToHTHotelProvided { get; set; }
        public string GTToHTName { get; set; }
        public string GTToHTPhone { get; set; }
        public int GTToHTTransitTime { get; set; }
        public DateTime GTToHTPickupTimeLocal { get; set; }

        public bool GTFromHTHotelProvided { get; set; }
        public string GTFromHTName { get; set; }
        public string GTFromHTPhone { get; set; }
        public int GTFromHTTransitTime { get; set; }
        public DateTime GTFromHTPickupTimeLocal { get; set; }

        public DateTime ArrivalFlightDateLocal { get; set; }
        public string ArrivalFlightNumber { get; set; }
        public string ArrivalAirportCode { get; set; }
        public string ArrivalAirlineCode { get; set; }

        public DateTime DepartureFlightDateLocal { get; set; }
        public string DepartureFlightNumber { get; set; }
        public string DepartureAirportCode { get; set; }
        public string DepartureAirlineCode { get; set; }
        public ReservationTypeEnum ResvType { get; set; }

        public SubmitHNNRequestEnum HNN_SubmitHNNRequest { get; set; }
        public int HNN_RequestId { get; set; }
        public string HNN_SubmittedBy { get; set; }
        public DateTime HNN_SubmittedDateTimeBaseLocal { get; set; }
        public WithdrawHNNRequestEnum HNN_WithdrawHNNRequest { get; set; }

        public bool IsATATransfer { get; set; }
        public string ATA_PickupAirportCode { get; set; }
        public string ATA_DropoffAirportCode { get; set; }
        public DateTime ATA_PickupDateTimeLocal { get; set; }
        public string ATA_GTName { get; set; }
        public string ATA_GTPhone { get; set; }

        public bool IsHTHTransfer { get; set; }
        public HTHTransportationTypeGtProvider HTH_GTProvider { get; set; }
        public DateTime HTH_PickupDateTimeLocal { get; set; }
        public int HTH_PickupHotelId { get; set; }
        public string HTH_PickupHotelName { get; set; }
        public string HTH_PickupHotelAddress { get; set; }
        public string HTH_PickupHotelPhone { get; set; }
        public int HTH_DropoffHotelId { get; set; }
        public string HTH_DropoffHotelName { get; set; }
        public string HTH_DropoffHotelAddress { get; set; }
        public string HTH_DropoffHotelPhone { get; set; }
        public string HTH_GTName { get; set; }
        public string HTH_GTPhone { get; set; }
    }
}