using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class QuotesDTO
    {
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Quotes { get; set; }

        public QuotesDTO(string source, DateTime date, Dictionary<string, decimal> quotes)
        {
            Source = source;
            Date = date;
            Quotes = quotes;
        }
    }
}
