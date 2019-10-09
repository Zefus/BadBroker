using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
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

                IEnumerable<DateTime> cachedDates = dates.Intersect(await _dBService.SelectRates<RatesData, DateTime>(qd => qd.Date));
                IEnumerable<DateTime> apiDates = dates.Except(await _dBService.SelectRates<RatesData, DateTime>(qd => qd.Date));

                List<RatesDTO> cachedRates = new List<RatesDTO>();

                IEnumerable<RatesData> ratesDatas;

                if (cachedDates.Count() != 0)
                {
                    ratesDatas = await _dBService.GetRates<RatesData>(qd => cachedDates.Contains(qd.Date), qd => qd.RatesPerDate);

                    foreach (RatesData ratesData in ratesDatas)
                    {
                        Dictionary<string, decimal> mappedRates = new Dictionary<string, decimal>();

                        ratesData.RatesPerDate.ForEach(rpd => mappedRates.Add(rpd.Name, rpd.Rate));

                        cachedRates.Add(new RatesDTO(ratesData.Date, mappedRates));
                    }
                }

                List<RatesDTO> rates = new List<RatesDTO>();

                if (apiDates.Count() != 0)
                {
                    List<RatesDTO> apiRates = (await _httpService.GetCurrencyRatesAsync(apiDates)).ToList();
                    List<RatesData> ratesForCaching = new List<RatesData>();

                    apiRates.ForEach(aR =>
                    {
                        RatesData ratesData = new RatesData();
                        ratesData.Date = aR.Date;
                        ratesData.RatesPerDate = new List<RatesPerDate>();
                        foreach (var key in aR.Rates.Keys)
                        {
                            ratesData.RatesPerDate.Add(new RatesPerDate() { Name = key, Rate = aR.Rates[key] });
                        }
                        ratesForCaching.Add(ratesData);
                    });

                    await _dBService.AddRatesRange(ratesForCaching);
                    rates = apiRates.Union(cachedRates).OrderBy(q => q.Date).ToList();
                }
                else
                {
                    rates = cachedRates;
                }

                List<RatesDTO> orderedRates = rates.OrderBy(q => q.Date).ToList();
                OutputDTO bestCase = _bestCaseSearcher.SearchBestCase(orderedRates, score);
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
