using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;

namespace BadBroker.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start = new DateTime(2019, 03, 01);
            DateTime end = new DateTime(2019, 03, 10);

            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            //using (BadBrokerContext badBrokerContext = new BadBrokerContext())
            //{
            //    QuotesData quotesData = new QuotesData();
            //    quotesData.StartDate = "qqq";
            //    quotesData.EndDate = "www";

            //    badBrokerContext.QuotesData.Add(quotesData);
            //    badBrokerContext.SaveChanges();
            //}
        }
    }
}
