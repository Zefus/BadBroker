using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Services;
using Newtonsoft.Json;

namespace BadBroker.Terminal
{
    class Program
    {
        private const string ACCESS_KEY = "c322dc640d70be2026e7ae22dd41417c";

        static void Main(string[] args)
        {
            Task.Run(() => MainAsync(args));

            Console.ReadKey();
        }

        public async static void FakeData()
        {
            using (BadBrokerContext badBrokerContext = new BadBrokerContext())
            {
                EnumerateDaysBetweenDates enumerateDaysBetweenDates = new EnumerateDaysBetweenDates();
                IEnumerable<DateTime> dates = enumerateDaysBetweenDates.Execute(new DateTime(2014, 12, 1), new DateTime(2014, 12, 15));
                HttpService httpService = new HttpService();
                List<QuotesDTO> quotesDTO = (await httpService.GetCurrencyRatesAsync(dates)).ToList();

                List<QuotesData> quotesDatas = new List<QuotesData>();

                quotesDTO.ForEach(qD => 
                {
                    QuotesData quotesData = new QuotesData();
                    quotesData.Date = qD.Date;
                    quotesData.Source = qD.Source;
                    quotesData.Quotes = qD.Quotes;
                    quotesDatas.Add(quotesData);
                });

                await badBrokerContext.AddRangeAsync(quotesDatas);
                await badBrokerContext.SaveChangesAsync();
            }
        }

        static async void MainAsync(string[] args)
        {
            TradeService tradeService = new TradeService();

            InputDTO inputDTO = new InputDTO(new DateTime(2014, 12, 1), new DateTime(2014, 12, 31));

            OutputDTO outputDTO = await tradeService.MakeTrade(inputDTO);

            //DBService dBService = new DBService();

            //IEnumerable<QuotesData> quotesDatas = dBService.GetQuotes<QuotesData>();

            //IEnumerable<DateTime> dates = await dBService.SelectQuotes<QuotesData, DateTime>(qd => qd.Date);
        }
    }
}
