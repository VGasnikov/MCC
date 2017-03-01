using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MCC.Domain
{
    public class HotelRepository
    {
        public static string GetHotelNameById(Guid id, string format=null) //{City}, {Title}
        {
            var da = new SqlDataAdapter("SELECT * from vwHotels WHERE HotelId=@HotelId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@HotelId", id);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                return "";
            var r = dt.Rows[0];
            if (string.IsNullOrEmpty(format))
                return r["Title"].ToString();

            return r.Format(format);
        }

        public static Hotel GetHotelById(int id)
        {
            var da = new SqlDataAdapter("api_GetHotelDetails", MvcApplication.cnStr);            
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserId", Guid.Parse("45215CAE-8538-42BB-AEC8-04F6578F368F"));
            da.SelectCommand.Parameters.AddWithValue("@HotelAutoNumber", id);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", Audience.Current.Airline.Id);
            var ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables.Count == 0)
                return null;

            var hotel = new Hotel();
            InitHotel(ds.Tables[0], hotel);
            var taxonomyIds = ds.Tables[1].AsEnumerable().Select(x => (Guid)x[0]).ToList();
            hotel.Facilities = TaxonomyRepository.GetTaxonomiesByIds(taxonomyIds);
            hotel.AreaInformation = AreaInformationRepository.GetAreaInformation(ds.Tables[2]);
            if (ds.Tables[3].Rows.Count > 0)
                hotel.CrewTransportation = CrewTransportationRepository.GetCrewTransportation(ds.Tables[3].Rows[0]);
            else
                hotel.CrewTransportation = new CrewTransportation();
            if (ds.Tables[4].Rows.Count > 0)
                hotel.CrewBenefits = CrewBenefitsRepository.GetCrewBenefits(ds.Tables[4].Rows[0]);
            else
                hotel.CrewBenefits = new CrewBenefits();
            return hotel;
        }
        
        private static void InitHotel(DataTable dt, Hotel hotel)
        {
                var r = dt.Rows[0];

                hotel.IdGuid = (Guid)r["HotelId"];
                hotel.Id = (int)r["AutoNumber"];
                hotel.IntegrationId = (string)r["IntegrationId"];
                hotel.Name = r["Title"].ToString();
                hotel.Phone = r["Phone"].ToString();
                hotel.Fax = r["Fax"].ToString();
                hotel.Website = r["Website"].ToString();
                hotel.Logo01 = r["Logo01"].ToString();
                hotel.Logo02 = r["Logo02"].ToString();
                hotel.Address01 = r["Address01"].ToString();
                hotel.Address02 = r["Address02"].ToString();
                hotel.City = r["City"].ToString();
                hotel.State = r["State"].ToString();
                hotel.PostalCode= r["PostalCode"].ToString();
                hotel.Country = r["Country"].ToString();
                hotel.Images = new List<string>();
                if (!string.IsNullOrEmpty(r["Logo01"].ToString()))
                    hotel.Images.Add(r["Logo01"].ToString());
                if (!string.IsNullOrEmpty(r["Logo02"].ToString()))
                    hotel.Images.Add(r["Logo02"].ToString());
                if (!string.IsNullOrEmpty(r["Logo03"].ToString()))
                    hotel.Images.Add(r["Logo03"].ToString());
                if (!string.IsNullOrEmpty(r["Logo04"].ToString()))
                    hotel.Images.Add(r["Logo04"].ToString());
                if (!string.IsNullOrEmpty(r["Logo05"].ToString()))
                    hotel.Images.Add(r["Logo05"].ToString());
                if (!string.IsNullOrEmpty(r["Logo06"].ToString()))
                    hotel.Images.Add(r["Logo06"].ToString());
                if (!string.IsNullOrEmpty(r["Logo07"].ToString()))
                    hotel.Images.Add(r["Logo07"].ToString());
                if (!string.IsNullOrEmpty(r["Logo08"].ToString()))
                    hotel.Images.Add(r["Logo08"].ToString());

                hotel.NumberOfRooms = r["NumberOfRooms"].ToString();
                hotel.NumberOfFloors = r["NumberOfFloors"].ToString();
                hotel.RoomAccess = r["RoomAccess"].ToString();
                hotel.NumberOfLifts = r["NumberOfLifts"].ToString();
                hotel.YearBuilt = r["YearBuilt"].ToString();
                hotel.LastRenovationYear = r["LastRenovationYear"].ToString();
                hotel.NumberOfRoomsRenovated = r["NumberOfRoomsRenovated"].ToString();
                hotel.PublicSpaceRenovated = r["PublicSpaceRenovated"].ToString();
                hotel.PropertyType = r["PropertyType"].ToString();
                hotel.MarketTier = r["MarketTier"].ToString();

        }

        public static Dictionary<Guid,string> GetHotelsByAirline(Guid airlineId)
        {           
            var da = new SqlDataAdapter("api_GetHotelsByAirlineId", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            return dt.AsEnumerable().ToDictionary(x => (Guid)x["HotelId"],x=>x["Hotel"].ToString());
        }

        public static List<Models.CityAndHotelsModel> GetCityAndHotels(Guid airlineId)
        {           
            var da = new SqlDataAdapter("api_GetCityAndHotels", MvcApplication.cnStr);
            da.SelectCommand.Parameters.AddWithValue("@AirlineId", airlineId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            var dt = new DataTable();
            da.Fill(dt);
            var l=new List<Models.CityAndHotelsModel>();
            foreach (DataRow r in dt.Rows)
                l.Add(new Models.CityAndHotelsModel
                {
                    AutoNumber = (int)r["AutoNumber"],
                    City = r["City"].ToString(),
                    Country = r["Country"].ToString(),
                    State = r["State"].ToString(),
                    HotelName = r["Title"].ToString()
                });
            return l;
        }

    }
}