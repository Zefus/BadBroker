using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.DataAccess;
using BadBroker.DataAccess.Models;
using BadBroker.BusinessLogic.Exceptions;

namespace BadBroker.BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        /// <summary>
        /// Method that implementation trade logic.
        /// </summary>
        /// <param name="inputDTO">Input DTO object that contains trade start date and end date</param>
        /// <returns>Output DTO object that contains information about trade best case</returns>
        public async Task<OutputDTO> MakeTrade(InputDTO inputDTO)
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    StringToDateParser stringToDateParser = new StringToDateParser();

                    DateTime startDate = stringToDateParser.Parse(inputDTO.StartDate);
                    DateTime endDate = stringToDateParser.Parse(inputDTO.EndDate);

                    EnumerateDaysBetweenDates enumerateDaysBetweenDates = new EnumerateDaysBetweenDates();
                    IEnumerable<DateTime> dates = enumerateDaysBetweenDates.Execute(startDate, endDate);

                    DBService dBService = new DBService();

                    IEnumerable<DateTime> cachedDates = dates.Intersect(await dBService.SelectQuotes<QuotesData, DateTime>(qd => qd.Date));
                    IEnumerable<DateTime> apiDates = dates.Except(await dBService.SelectQuotes<QuotesData, DateTime>(qd => qd.Date));

                    List<QuotesDTO> cachedQuotes = new List<QuotesDTO>();

                    IEnumerable<QuotesData> quotesDatas = await dBService.GetQuotes<QuotesData>(qd => cachedDates.Contains(qd.Date));

                    foreach (QuotesData quotesData in quotesDatas)
                    {
                        cachedQuotes.Add(new QuotesDTO(quotesData.Date, quotesData.Quotes));
                    }

                    List<QuotesDTO> quotes = new List<QuotesDTO>();

                    if (apiDates.Count() != 0)
                    {
                        HttpService httpService = new HttpService();
                        List<QuotesDTO> apiQuotes = (await httpService.GetCurrencyRatesAsync(apiDates)).ToList();
                        List<QuotesData> quotesForCashing = new List<QuotesData>();

                        apiQuotes.ForEach(aQ =>
                        {
                            QuotesData quotesData = new QuotesData();
                            quotesData.Date = aQ.Date;
                            quotesData.Quotes = aQ.Quotes;
                            quotesForCashing.Add(quotesData);
                        });

                        await dBService.AddQuotesRange(quotesForCashing);
                        quotes = apiQuotes.Union(cachedQuotes).OrderBy(q => q.Date).ToList();
                    }
                    else
                    {
                        quotes = cachedQuotes;
                    }

                    BestCaseSearcher bestCaseSearcher = new BestCaseSearcher();
                    OutputDTO bestCase = bestCaseSearcher.SearchBestCase(quotes.OrderBy(q => q.Date).ToList());
                    return bestCase;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new TradeServiceException(ex.Message, ex);
            }
            catch (DBServiceException ex)
            {
                throw new TradeServiceException(ex.Message, ex);
            }
            catch (HttpServiceException ex)
            {
                throw new TradeServiceException(ex.Message, ex);
            }
        }
    }
}
