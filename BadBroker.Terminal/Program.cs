using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;
using BadBroker.BusinessLogic.ModelsDTO;
using Newtonsoft.Json;

namespace BadBroker.Terminal
{
    class Program
    {
        private const string ACCESS_KEY = "c322dc640d70be2026e7ae22dd41417c";

        static void Main(string[] args)
        {
            //using (BadBrokerContext badBrokerContext = new BadBrokerContext())
            //{
            //    QuotesData quotesData = new QuotesData();
            //    quotesData.Date = DateTime.Now;
            //    quotesData.Source = "RUB";

            //    badBrokerContext.QuotesData.Add(quotesData);
            //    badBrokerContext.SaveChanges();
            //}

            Console.WriteLine(DateTime.Now);
            Console.ReadKey();
        }
    }
}
