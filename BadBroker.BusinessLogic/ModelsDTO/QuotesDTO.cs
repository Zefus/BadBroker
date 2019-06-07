using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class QuotesDTO
    {
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Quotes { get; set; }

        public QuotesDTO(DateTime date, Dictionary<string, decimal> quotes)
        {
            Date = date;
            Quotes = quotes;
        }
    }
}
