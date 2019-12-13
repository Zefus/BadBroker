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
        private IExternalServiceClient _externalServiceClient;
        private IBestCaseSearcher _bestCaseSearcher;
        private IGetCachedRatesOperation _getCachedRatesOperation;

        public TradeService(IStringToDateParser            stringToDateParser, 
                            IEnumerateDaysBetweenDates     enumerateDaysBetweenDates, 
                            IDBService                     dBService, 
                            IExternalServiceClient         externalServiceClient,
                            IBestCaseSearcher              bestCaseSearcher,
                            IGetCachedRatesOperation       getCachedRatesOperation)
        {
            _stringToDateParser = stringToDateParser;
            _enumerateDaysBetweenDates = enumerateDaysBetweenDates;
            _dBService = dBService;
            _externalServiceClient = externalServiceClient;
            _bestCaseSearcher = bestCaseSearcher;
            _getCachedRatesOperation = getCachedRatesOperation;
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
                DateTime startDate = _stringToDateParser.Parse(inputDTO.StartDate);

                DateTime endDate = _stringToDateParser.Parse(inputDTO.EndDate);

                decimal score = inputDTO.Score;

                IEnumerable<DateTime> dates = _enumerateDaysBetweenDates.Execute(startDate, endDate);

                IEnumerable<DateTime> cachedDates = dates.Intersect(await _dBService.SelectRates<RatesData, DateTime>(rd => rd.Date));

                IEnumerable<DateTime> apiDates = dates.Except(await _dBService.SelectRates<RatesData, DateTime>(rd => rd.Date));

                IEnumerable<RatesDTO> cachedRates = await _getCachedRatesOperation.ExecuteAsync(cachedDates);

                IEnumerable<RatesDTO> apiRates = await _externalServiceClient.GetCurrencyRatesAsync(apiDates);

                List<RatesData> ratesForCaching = new List<RatesData>();

                if (apiRates.Count() != 0)
                {                    
                    foreach (RatesDTO aR in apiRates)
                    {
                        RatesData ratesData = new RatesData();
                        ratesData.Date = aR.Date;
                        ratesData.RatesPerDate = new List<RatesPerDate>();
                        foreach (var key in aR.Rates.Keys)
                        {
                            ratesData.RatesPerDate.Add(new RatesPerDate() { Name = key, Rate = aR.Rates[key] });
                        }
                        ratesForCaching.Add(ratesData);
                    }
                }
                await _dBService.AddRatesRange(ratesForCaching);

                List<RatesDTO> rates = apiRates.Union(cachedRates).OrderBy(r => r.Date).ToList();

                OutputDTO bestCase = _bestCaseSearcher.SearchBestCase(rates, score);

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
        }
    }
}
