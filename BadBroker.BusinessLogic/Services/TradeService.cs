using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.DataAccess.Models;
using BadBroker.BusinessLogic.Exceptions;

namespace BadBroker.BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        private IStringToDateParser _stringToDateParser;
        private IEnumerateDaysBetweenDates _enumerateDaysBetweenDates;
        private IDBService _dBService;
        private IHttpService _httpService;
        private IBestCaseSearcher _bestCaseSearcher;

        public TradeService(IStringToDateParser        stringToDateParser, 
                            IEnumerateDaysBetweenDates enumerateDaysBetweenDates, 
                            IDBService                 dBService, 
                            IHttpService               httpService,
                            IBestCaseSearcher          bestCaseSearcher)
        {
            _stringToDateParser = stringToDateParser;
            _enumerateDaysBetweenDates = enumerateDaysBetweenDates;
            _dBService = dBService;
            _httpService = httpService;
            _bestCaseSearcher = bestCaseSearcher;
        }

        /// <summary>
        /// Method that implementation trade logic.
        /// </summary>
        /// <param name="inputDTO">Input DTO object that contains trade start date and end date</param>
        /// <returns>Output DTO object that contains information about trade best case</returns>
        public async Task<OutputDTO> MakeTrade(InputDTO inputDTO)
        {
            try
            {
                if (_stringToDateParser == null)
                {
                    throw new NullReferenceException("_stringToDateParser field is null");
                }

                if (_enumerateDaysBetweenDates == null)
                {
                    throw new NullReferenceException("_enumerateDaysBetweenDates field is null");
                }

                if (_httpService == null)
                {
                    throw new NullReferenceException("_httpService field is null");
                }

                if (_dBService == null)
                {
                    throw new NullReferenceException("_dBService field is null");
                }

                DateTime startDate = _stringToDateParser.Parse(inputDTO.StartDate);
                DateTime endDate = _stringToDateParser.Parse(inputDTO.EndDate);
                decimal score = inputDTO.Score;

                IEnumerable<DateTime> dates = _enumerateDaysBetweenDates.Execute(startDate, endDate);

                IEnumerable<DateTime> cachedDates = dates.Intersect(await _dBService.SelectQuotes<QuotesData, DateTime>(qd => qd.Date));
                IEnumerable<DateTime> apiDates = dates.Except(await _dBService.SelectQuotes<QuotesData, DateTime>(qd => qd.Date));

                List<QuotesDTO> cachedQuotes = new List<QuotesDTO>();

                IEnumerable<QuotesData> quotesDatas;

                if (cachedDates.Count() != 0)
                {
                    quotesDatas = await _dBService.GetQuotes<QuotesData>(qd => cachedDates.Contains(qd.Date));

                    foreach (QuotesData quotesData in quotesDatas)
                    {
                        cachedQuotes.Add(new QuotesDTO(quotesData.Date, quotesData.Quotes));
                    }
                }

                List<QuotesDTO> quotes = new List<QuotesDTO>();

                if (apiDates.Count() != 0)
                {
                    List<QuotesDTO> apiQuotes = (await _httpService.GetCurrencyRatesAsync(apiDates)).ToList();
                    List<QuotesData> quotesForCashing = new List<QuotesData>();

                    apiQuotes.ForEach(aQ =>
                    {
                         QuotesData quotesData = new QuotesData();
                         quotesData.Date = aQ.Date;
                         quotesData.Quotes = aQ.Quotes;
                         quotesForCashing.Add(quotesData);
                    });

                    await _dBService.AddQuotesRange(quotesForCashing);
                    quotes = apiQuotes.Union(cachedQuotes).OrderBy(q => q.Date).ToList();
                }
                else
                {
                    quotes = cachedQuotes;
                }

                OutputDTO bestCase = _bestCaseSearcher.SearchBestCase(quotes.OrderBy(q => q.Date).ToList(), score);
                return bestCase;
            }
            catch (NullReferenceException ex)
            {
                throw new TradeServiceException(ex.Message, ex);
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
            catch (Exception ex)
            {
                throw new TradeServiceException(ex.Message, ex);
            } 
        }
    }
}
