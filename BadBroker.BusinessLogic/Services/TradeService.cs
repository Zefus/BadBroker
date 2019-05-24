using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;

namespace BadBroker.BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        public async Task<OutputDTO> MakeTrade(InputDTO inputDTO)
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    EnumerateDaysBetweenDates enumerateDaysBetweenDates = new EnumerateDaysBetweenDates();
                    IEnumerable<DateTime> dates = enumerateDaysBetweenDates.Execute(inputDTO.StartDate, inputDTO.EndDate);

                    //dates from DB
                    IEnumerable<DateTime> cachedDates = dates.Intersect(db.QuotesData.Select(qd => qd.Date));
                    //dates from API
                    IEnumerable<DateTime> apiDates = dates.Except(db.QuotesData.Select(qd => qd.Date));
                    //quotes from DB
                    List<QuotesDTO> cachedQuotes = new List<QuotesDTO>();

                    IQueryable<QuotesData> quotesDatas = await Task.Run(() => db.QuotesData.Where(qd => cachedDates.Contains(qd.Date)));

                    foreach (QuotesData quotesData in quotesDatas)
                    {
                        cachedQuotes.Add(new QuotesDTO(quotesData.Source, quotesData.Date, quotesData.Quotes));
                    }

                    HttpService httpService = new HttpService();
                    //quotes from API
                    List<QuotesDTO> apiQuotes = (await httpService.GetCurrencyRatesAsync(apiDates)).ToList();

                    apiQuotes.ForEach(async aQ =>
                    {
                        QuotesData quotesData = new QuotesData();
                        quotesData.Source = aQ.Source;
                        quotesData.Date = aQ.Date;
                        quotesData.Quotes = aQ.Quotes;
                        await db.QuotesData.AddAsync(quotesData);
                    });

                    List<QuotesDTO> quotes = apiQuotes.Union(cachedQuotes).OrderBy(q => q.Date).ToList();

                    BestCaseSearcher bestCaseSearcher = new BestCaseSearcher();
                    OutputDTO bestCase = bestCaseSearcher.SearchBestCase(quotes);
                    return bestCase;
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }
    }
}
