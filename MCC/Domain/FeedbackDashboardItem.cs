using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MCC.Domain
{
    public class FeedbackDashboardItem
    {
        [JsonProperty(PropertyName = "a")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string FeedbackType { get; set; }

        [JsonProperty(PropertyName = "c")]
        public DateTime EntryDate { get; set; }

        [JsonProperty(PropertyName = "d")]
        public string HotelTransportationName { get; set; }

        [JsonProperty(PropertyName = "e")]
        public string Topic { get; set; }

        [JsonProperty(PropertyName = "f")]
        public string ResolutionDate { get; set; }

        [JsonProperty(PropertyName = "g")]
        public string Priority { get; set; }

        [JsonProperty(PropertyName = "h")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "i")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "j")]
        public string Status { get; set; }


    }
}