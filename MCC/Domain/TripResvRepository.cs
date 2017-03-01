using System;
using System.Collections.Generic;
using System.Linq;
using MCC.MyTravelServiceReference;

namespace MCC.Domain
{
    public class TripResvRepository
    {
        public static List<TripResv> GetTrips(string airlineCode, string employeeId, DateTime startDateTimeL, DateTime endDateTimeL)
        {
            var serv = new MyTravelSoapClient();
            serv.ClientCredentials.UserName.UserName = System.Web.Configuration.WebConfigurationManager.AppSettings["MyTravel.Service.UserName"];
            serv.ClientCredentials.UserName.Password = System.Web.Configuration.WebConfigurationManager.AppSettings["MyTravel.Service.pwd"];

            DateTime endDateTimeWithTime = endDateTimeL.AddHours(23).AddMinutes(59);

            var LayoverDataRequest = new LayoverDataRequest { crewId = employeeId, airlineCode = airlineCode, startDateTimeL = startDateTimeL, endDateTimeL = endDateTimeWithTime };
            var result = serv.GetLayoverData(LayoverDataRequest);

            if (result.statusCode != StatusEnum.SUCCESS)
                return new List<TripResv>();

            List<TripResv> Trips = new List<TripResv>();
            if (result.layovers.layover.Count() > 0)
            {
                foreach (LayoverType layover in result.layovers.layover)
                {
                    TripResv trip = new TripResv();
                    trip.CrewId = employeeId;
                    trip.ConfirmationNumber = String.IsNullOrEmpty(layover.confirmationNumber) ? "" : layover.confirmationNumber;
                    trip.ReservationStatus = layover.reservationStatus;
                    trip.PairingNumber = layover.pairingNumber;
                    trip.AirportCity = layover.airportCity;
                    trip.AirportCode = layover.airportCode;
                    trip.TripReservationId = layover.reservationId;
                    if (layover.hotelDetails != null)
                    {
                        trip.HotelName = layover.hotelDetails.hotelName;
                        trip.HotelPhone = layover.hotelDetails.hotelPhone;
                        trip.HotelAddress = layover.hotelDetails.hotelAddress;
                        trip.HotelSupplierId = layover.hotelDetails.hotelId;
                    }


                    DateTimeOffset checkIndateTimeOffset = new DateTimeOffset(layover.checkInDateTimeL);
                    trip.CheckInDateTimeLocal = checkIndateTimeOffset.DateTime;
                    trip.TripDateUTC = checkIndateTimeOffset.UtcDateTime;
                    trip.TripDateLocal = checkIndateTimeOffset.DateTime;
                    DateTimeOffset checkOutdateTimeOffset = new DateTimeOffset(layover.checkOutDateTimeL);
                    trip.CheckOutDateTimeLocal = checkOutdateTimeOffset.DateTime;
                    if (layover.toHotelGroundTransportation != null)
                    {
                        trip.GTToHTHotelProvided = layover.toHotelGroundTransportation.hotelProvided;
                        if (trip.GTToHTHotelProvided)
                        {
                            trip.GTToHTName = "Use Hotel Shuttle";
                            trip.GTToHTPhone = trip.HotelPhone;
                        }
                        else if (layover.toHotelGroundTransportation.Item != null)
                        {
                            trip.GTToHTName = layover.toHotelGroundTransportation.Item.name;
                            trip.GTToHTPhone = layover.toHotelGroundTransportation.Item.phoneNumber;
                        }
                        trip.GTToHTTransitTime = layover.toHotelGroundTransportation.transitTime;
                        DateTimeOffset GroundTransportationDateTimeOffset = new DateTimeOffset(layover.toHotelGroundTransportation.pickupDateTimeL);
                        trip.GTToHTPickupTimeLocal = GroundTransportationDateTimeOffset.DateTime;
                    }
                    if (layover.fromHotelGroundTransportation != null)
                    {
                        trip.GTFromHTHotelProvided = layover.fromHotelGroundTransportation.hotelProvided;
                        if (trip.GTFromHTHotelProvided)
                        {
                            trip.GTFromHTName = "Use Hotel Shuttle";
                            trip.GTFromHTPhone = trip.HotelPhone;
                        }
                        else if (layover.fromHotelGroundTransportation.Item != null)
                        {
                            trip.GTFromHTName = layover.fromHotelGroundTransportation.Item.name;
                            trip.GTFromHTPhone = layover.fromHotelGroundTransportation.Item.phoneNumber;
                        }
                        trip.GTFromHTTransitTime = layover.fromHotelGroundTransportation.transitTime;
                        DateTimeOffset FromGroundTransportationDateTimeOffset = new DateTimeOffset(layover.fromHotelGroundTransportation.pickupDateTimeL);
                        trip.GTFromHTPickupTimeLocal = FromGroundTransportationDateTimeOffset.DateTime;
                    }
                    if (layover.arrivalFlight.arrivalDateTimeL != null)
                    {
                        DateTimeOffset arrivalDateTimeOffset = new DateTimeOffset(layover.arrivalFlight.arrivalDateTimeL);
                        trip.ArrivalFlightDateLocal = arrivalDateTimeOffset.DateTime;
                    }
                    trip.ArrivalFlightNumber = layover.arrivalFlight.flightNumber;
                    trip.ArrivalAirportCode = layover.arrivalFlight.arrivalAirportCode;
                    trip.ArrivalAirlineCode = layover.arrivalFlight.airlineCode;
                    if (layover.departureFlight.departureDateTimeL != null)
                    {
                        DateTimeOffset departureDateTimeOffset = new DateTimeOffset(layover.departureFlight.departureDateTimeL);
                        trip.DepartureFlightDateLocal = departureDateTimeOffset.DateTime;
                    }
                    trip.DepartureFlightNumber = layover.departureFlight.flightNumber;
                    trip.DepartureAirportCode = layover.departureFlight.departureAirportCode;
                    trip.DepartureAirlineCode = layover.departureFlight.airlineCode;
                    trip.ResvType = layover.reservationType;

                    trip.HNN_SubmitHNNRequest = layover.hotelNotNeeded.submitHNNRequest;
                    if (layover.hotelNotNeeded.hnnRequest != null)
                    {
                        trip.HNN_RequestId = layover.hotelNotNeeded.hnnRequest.hnnRequestId;
                        trip.HNN_SubmittedBy = layover.hotelNotNeeded.hnnRequest.submittedBy;
                        trip.HNN_WithdrawHNNRequest = layover.hotelNotNeeded.hnnRequest.withdrawHNNRequest;
                        if (layover.hotelNotNeeded.hnnRequest.submittedDateTimeBL != null)
                        {
                            DateTimeOffset hnnRequestSubmittedDateTimeOffset = new DateTimeOffset(layover.hotelNotNeeded.hnnRequest.submittedDateTimeBL);
                            trip.HNN_SubmittedDateTimeBaseLocal = hnnRequestSubmittedDateTimeOffset.DateTime;
                        }
                    }
                    Trips.Add(trip);
                }
            }
            if (result.ataGroundSegments != null && result.ataGroundSegments.ataGroundSegment.Count() > 0)
            {
                foreach (ATATransportationType GTSegment in result.ataGroundSegments.ataGroundSegment)
                {
                    TripResv trip = new TripResv();
                    trip.CrewId = employeeId;
                    trip.IsATATransfer = true;
                    trip.HotelName = "Unassigned";
                    if (GTSegment.pickupDateTimeL != null)
                    {
                        DateTimeOffset pickupDateTimeOffset = new DateTimeOffset(GTSegment.pickupDateTimeL);
                        trip.TripDateLocal = pickupDateTimeOffset.DateTime;
                        trip.TripDateUTC = pickupDateTimeOffset.UtcDateTime;
                    }
                    trip.ATA_PickupAirportCode = GTSegment.pickupAirportCode;
                    trip.AirportCode = GTSegment.pickupAirportCode;
                    trip.ATA_DropoffAirportCode = GTSegment.dropoffAirportCode;
                    trip.ATA_PickupDateTimeLocal = trip.TripDateLocal;
                    if (GTSegment.Item != null)
                    {
                        trip.ATA_GTName = GTSegment.Item.name;
                        trip.ATA_GTPhone = GTSegment.Item.phoneNumber;
                    }
                    Random rnd = new Random();
                    trip.TripReservationId = rnd.Next(100000, 999999);
                    Trips.Add(trip);
                }
            }
            if (result.hthGroundSegments != null && result.hthGroundSegments.hthGroundSegment.Count() > 0)
            {
                foreach (HTHTransportationType hthGroundSegment in result.hthGroundSegments.hthGroundSegment)
                {
                    TripResv trip = new TripResv();
                    trip.CrewId = employeeId;
                    trip.IsHTHTransfer = true;
                    trip.HTH_GTProvider = hthGroundSegment.gtProvider;
                    DateTimeOffset hthpickupDateTimeOffset = new DateTimeOffset(hthGroundSegment.pickupDateTimeL);
                    trip.HTH_PickupDateTimeLocal = hthpickupDateTimeOffset.DateTime;

                    //Begin Victor
                    //trip.HTH_PickupHotelId = hthGroundSegment.pickupHotel.hotelId;
                    //trip.HTH_PickupHotelName = hthGroundSegment.pickupHotel.hotelName;
                    //trip.HTH_PickupHotelAddress = hthGroundSegment.pickupHotel.hotelAddress;
                    //trip.HTH_PickupHotelPhone = hthGroundSegment.pickupHotel.hotelPhone;
                    //trip.HTH_DropoffHotelId = hthGroundSegment.dropoffHotel.hotelId;
                    //trip.HTH_DropoffHotelName = hthGroundSegment.dropoffHotel.hotelName;
                    //trip.HTH_DropoffHotelAddress = hthGroundSegment.dropoffHotel.hotelAddress;
                    //trip.HTH_DropoffHotelPhone = hthGroundSegment.dropoffHotel.hotelPhone;
                    //End Victor

                    trip.HTH_GTName = hthGroundSegment.transportationProvider.name;
                    trip.HTH_GTPhone = hthGroundSegment.transportationProvider.phoneNumber;
                    Trips.Add(trip);
                }
            }

            return Trips;
        }
    }
}