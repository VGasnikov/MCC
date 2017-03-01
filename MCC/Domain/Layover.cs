using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCC.Domain
{
    public class Layover
    {
        public string PairingId { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public string AirportCode { get; set; }
        public string Phone { get; set; }
        public string ConfirmationNumber { get; set; }

    }
}