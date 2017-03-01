using System.Data.SqlClient;
using System.Data;

namespace MCC.Domain
{
    public class CrewTransportationRepository
    {
        public static CrewTransportation GetCrewTransportation(string integrationId)
        {
            var da = new SqlDataAdapter("SELECT TOP 1 * FROM vwCrewTransportations WHERE IntegrationId = @Id", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@Id", integrationId);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count == 0 ? null : GetCrewTransportation(dt.Rows[0]);
        }

        public static CrewTransportation GetCrewTransportation(DataRow r)
        {
            var o = new CrewTransportation();
            o.IntegrationId = r["IntegrationId"].ToString();
            o.Details = r["Details"].ToString();
            o.ShuttleHoursOfOperationFrom = r["ShuttleHoursOfOperationFrom"].ToString();
            o.ShuttleHoursOfOperationUntil = r["ShuttleHoursOfOperationUntil"].ToString();
            o.CompanyName = r["CompanyName"].ToString();
            o.PhoneNumber = r["PhoneNumber"].ToString();
            o.CrewTransportIsConductedByHotel = (bool)r["CrewTransportIsConductedByHotel"];
            o.TransportIsConductedByOutsideService = (bool)r["TransportIsConductedByOutsideService"];
            o.Shuttle24HoursOfOperation = (bool)r["Shuttle24HoursOfOperation"];
            o.CarRentalAtHotel = (bool)r["CarRentalAtHotel"];
            o.GroundTransportTime = r["GroundTransportTime"].ToString();
            o.TaxiCarServiceCompany = r["TaxiCarServiceCompany"].ToString();
            o.TaxiCarServiceCompanyPhone = r["TaxiCarServiceCompanyPhone"].ToString();
            o.CarRentalCompany = r["CarRentalCompany"].ToString();
            o.CarRentalCompanyPhone = r["CarRentalCompanyPhone"].ToString();              
            return o;
        }
    }
}