using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BadBroker.DataAccess.Models
{
    public class QuotesData
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [NotMapped]
        public Dictionary<string, decimal> Rates { get; set; }
        [Column("Rates")]
        public string RatesJson
        {
            get => JsonConvert.SerializeObject(Rates);
            set
            {
                Rates = (Dictionary<string, decimal>)JsonConvert.DeserializeObject(value);
            }
        }
    }
}
