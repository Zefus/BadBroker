using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class RatesDTO
    {
        public bool Success { get; set; }
        public DateTime Date { get; set; }
        public ErrorModelDTO Error { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }

        public RatesDTO(DateTime date, Dictionary<string, decimal> rates)
        {
            Date = date;
            Rates = rates;
        }
    }
}
