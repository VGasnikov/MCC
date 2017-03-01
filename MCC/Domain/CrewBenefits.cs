using System;
namespace MCC.Domain
{
    public class CrewBenefits
    {
        public Guid CrewBenefitId { get; set; }
        public string IntegrationId { get; set; }
        public string CrewLoungeDetails { get; set; }
        public string FoodBeverageDiscount { get; set; }
        public string AirlineLeisureRate { get; set; }
        public string CheckCashingDetails { get; set; }
        public string BreakfastPrice { get; set; }
        public string FoodBevDiscount { get; set; }
        public string DryCleaningDiscount { get; set; }
        public string HighSpeedInternetCostforCrew { get; set; }
        public string PercentageDiscountonCalls { get; set; }
        public string HealthFitnessCenterAmount { get; set; }
        public string CheckCashingAmount { get; set; }
        public string Misc { get; set; }
        public bool BreakfastComplimentary { get; set; }
        public bool InRoomMineralWaterComplimentary { get; set; }
        public bool RefreshmentsInLobbyAvailable { get; set; }
        public bool DryCleaningComplimentary { get; set; }
        public bool WiFiComplimentary { get; set; }
        public bool TollFreeCallsComplimentary { get; set; }
        public bool LocalCallsComplimentary { get; set; }
        public bool CrewLoungeComplimentary { get; set; }
        public bool NewspaperComplimentary { get; set; }
        public bool HealthFitnessCenterComplimentary { get; set; }
        public bool SeparateCheckInAreaComplimentary { get; set; }
        public bool CheckCashingFlightAvailable { get; set; }
        public bool RefreshmentsInLobbyComplimentary { get; set; }
        public bool HighSpeedInternetComplimentary { get; set; }
    }
}