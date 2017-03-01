using System;
using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class CrewBenefitsRepository
    {
        public static CrewBenefits GetCrewBenefits(string integrationId)
        {
            var da = new SqlDataAdapter("SELECT TOP 1 * FROM vwCrewBenefits WHERE IntegrationId = @Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", integrationId);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count == 0 ? null : GetCrewBenefits(dt.Rows[0]);
        }

        public static CrewBenefits GetCrewBenefits(DataRow r)
        {
            var o = new CrewBenefits();
            o.CrewBenefitId = (Guid)r["CrewBenefitId"];
            o.IntegrationId = r["IntegrationId"].ToString();
            o.CrewLoungeDetails = r["CrewLoungeDetails"].ToString();
            o.FoodBeverageDiscount = r["FoodBeverageDiscount"].ToString();
            o.AirlineLeisureRate = r["AirlineLeisureRate"].ToString();
            o.CheckCashingDetails = r["CheckCashingDetails"].ToString();
            o.BreakfastPrice = r["BreakfastPrice"].ToString();
            o.FoodBevDiscount = r["FoodBevDiscount"].ToString();
            o.DryCleaningDiscount = r["DryCleaningDiscount"].ToString();
            o.HighSpeedInternetCostforCrew = r["HighSpeedInternetCostforCrew"].ToString();
            o.PercentageDiscountonCalls = r["PercentageDiscountonCalls"].ToString();
            o.HealthFitnessCenterAmount = r["HealthFitnessCenterAmount"].ToString();
            o.CheckCashingAmount = r["CheckCashingAmount"].ToString();
            o.Misc = r["Misc"].ToString();
            o.BreakfastComplimentary = (bool)r["BreakfastComplimentary"];
            o.InRoomMineralWaterComplimentary = (bool)r["InRoomMineralWaterComplimentary"];
            o.RefreshmentsInLobbyAvailable = (bool)r["RefreshmentsInLobbyAvailable"];
            o.DryCleaningComplimentary = (bool)r["DryCleaningComplimentary"];
            o.WiFiComplimentary = (bool)r["WiFiComplimentary"];
            o.TollFreeCallsComplimentary = (bool)r["TollFreeCallsComplimentary"];
            o.LocalCallsComplimentary = (bool)r["LocalCallsComplimentary"];
            o.CrewLoungeComplimentary = (bool)r["CrewLoungeComplimentary"];
            o.NewspaperComplimentary = (bool)r["NewspaperComplimentary"];
            o.HealthFitnessCenterComplimentary = (bool)r["HealthFitnessCenterComplimentary"];
            o.SeparateCheckInAreaComplimentary = (bool)r["SeparateCheckInAreaComplimentary"];
            o.CheckCashingFlightAvailable = (bool)r["CheckCashingFlightAvailable"];
            o.RefreshmentsInLobbyComplimentary = (bool)r["RefreshmentsInLobbyComplimentary"];
            o.HighSpeedInternetComplimentary = (bool)r["HighSpeedInternetComplimentary"];
            return o;
        }
    }
}