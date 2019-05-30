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
        public DateTime Date { get; set; }
        public string Source { get; set; }
        [NotMapped]
        public Dictionary<string, decimal> Quotes { get; set; }
        [Column("Rates")]
        public string QuotesJson
        {
            get => JsonConvert.SerializeObject(Quotes);
            set
            {
                Quotes = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(value);
            }
        }
    }
}
