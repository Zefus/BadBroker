using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadBroker.Web.Models
{
    public class RatesViewModel
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}