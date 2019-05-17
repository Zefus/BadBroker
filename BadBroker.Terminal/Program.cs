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

            Task.Run(() => met());
            //using (BadBrokerContext badBrokerContext = new BadBrokerContext())
            //{
            //    QuotesData quotesData = new QuotesData();
            //    quotesData.StartDate = "qqq";
            //    quotesData.EndDate = "www";

            //    badBrokerContext.QuotesData.Add(quotesData);
            //    badBrokerContext.SaveChanges();
            //}
            Console.ReadKey();
        }

        public static async void met()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"http://apilayer.net/api/historical?access_key=c322dc640d70be2026e7ae22dd41417c&date=2015-05-01&currencies=RUB,EUR,GBP,JPY&format=1");
                    response.EnsureSuccessStatusCode();  
                    string responseBody = await response.Content.ReadAsStringAsync();
                    QuotesDTO result = JsonConvert.DeserializeObject<QuotesDTO>(responseBody);
                }
                catch (HttpRequestException e)
                {

                }
            }
        }
    }
}
