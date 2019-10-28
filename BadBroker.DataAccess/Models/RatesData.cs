using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BadBroker.DataAccess.Models
{
    public class RatesData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<RatesPerDate> RatesPerDate { get; set; }
    }
}
