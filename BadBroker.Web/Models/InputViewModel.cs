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
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}