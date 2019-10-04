﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.DataAccess.Models
{
    public class RatesPerDate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public int QuotesDataId { get; set; }
        public QuotesData QuotesData { get; set; }
    }
}
