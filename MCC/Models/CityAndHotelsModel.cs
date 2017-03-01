using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MCC.Models
{
    public class CityAndHotelsModel
    {
        [JsonProperty(PropertyName = "a")]
        public int AutoNumber { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "c")]
        public string HotelName { get; set; }

        [JsonProperty(PropertyName = "d")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "e")]
        public string Country { get; set; }
    }
}