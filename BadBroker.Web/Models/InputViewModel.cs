using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadBroker.Web.Models
{
    public class InputViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<RatesViewModel> AllRates { get; set; }
    }
}