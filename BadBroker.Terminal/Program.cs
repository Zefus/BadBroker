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

            string day = start.Day < 10 ? $"0{start.Day}" : start.Day.ToString();

            string month = start.Month < 10 ? $"0{start.Month}" : start.Month.ToString();

            string str = $"{start.Day}-{start.Month}-{start.Year}";

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
