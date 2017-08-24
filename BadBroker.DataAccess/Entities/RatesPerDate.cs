using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace BadBroker.DataAccess.Entities
{
    public class RatesPerDate
    {
        public int ID { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public Dictionary<string, decimal> Rates { get; set; }
        [Column("Rates")]
        public string RatesJson
        {
            get => JsonConvert.SerializeObject(Rates);
            set
            {
                Rates = (Dictionary<string, decimal>) JsonConvert.DeserializeObject(value);
            }
        }
    }
}
