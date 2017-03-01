using System;
using System.Web;

namespace MCC.Domain
{
    public class Audience
    {
        const string _DefaultLanguage = "en";
        public static Audience Current
        {
            get
            {
                return (Audience)HttpContext.Current.Items["Audience"];
            }
            set
            {
                HttpContext.Current.Items["Audience"] = value;
            }
        }


        public string Language { get; set; }
        public string AudienceName { get;}

        private Lazy<Airline> _airline;
        public Airline Airline
        {
            get
            {
                return _airline.Value;
            }
        }

        public Audience(string audienceName)
        {
            Language = GetLanguage(audienceName);
            AudienceName = GetAudienceName(audienceName);

            _airline = new Lazy<Airline>(() => {
                var airlineId =AirlineRepository.GetAirlineIdByAudienceName(AudienceName);
                if (!airlineId.HasValue)
                    return null;
                return AirlineRepository.GetAirlineById(airlineId.Value);
            });
        }

        public override string ToString()
        {
            return GetLocalizedAudienceName(Language);
        }

        public string GetLocalizedAudienceName(string language)
        {
            if (language == _DefaultLanguage)
                return AudienceName;
            return AudienceName + "-" + language;
        }

        private static string GetLanguage(string audience)
        {
            var m = System.Text.RegularExpressions.Regex.Match(audience, @"-\D\D$");
            if (!m.Success)
                return "en";
            return m.Value.Substring(1).ToLower();
        }

        private string GetAudienceName(string audience)
        {
            if (string.IsNullOrEmpty(audience))
                return "";
            var m = System.Text.RegularExpressions.Regex.Match(audience, @"-\D\D$");
            if (m.Success)
                return audience.Substring(0, audience.Length - 3);
            return audience;
        }
    }
}