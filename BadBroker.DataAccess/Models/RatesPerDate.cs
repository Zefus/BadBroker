using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadBroker.DataAccess.Models
{
    public class RatesPerDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        public int RatesDataId { get; set; }
        public RatesData RatesData { get; set; }
    }
}
