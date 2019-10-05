using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.Extensions;
using BadBroker.BusinessLogic.Exceptions;
using Newtonsoft.Json;

namespace BadBroker.BusinessLogic.Services
{
    public class ExchangeRatesApiClient : IHttpService
    {
        /// <summary>
        /// Method return data about currency rates from apilayer.net by API service.
        /// </summary>
        /// <param name="dates">Dates on which currency data will be received</param>
        /// <returns>Return the collection of currency data</returns>
        public async Task<IEnumerable<RatesDTO>> GetCurrencyRatesAsync(IEnumerable<DateTime> dates)
        {
            try
            {
                List<RatesDTO> quotes = new List<RatesDTO>();

                HttpResponseMessage response;

                using (HttpClient client = new HttpClient())
                {
                    foreach (DateTime date in dates)
                    {                        
                        response = await client.GetAsync($"https://api.exchangeratesapi.io/date={date.ToApiStringFormat()}?base=USD&symbols=RUB,EUR,GBP,JPY");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        RatesDTO result = JsonConvert.DeserializeObject<RatesDTO>(responseBody);
                        if (result.Success)
                            quotes.Add(result);
                        else
                            throw new HttpServiceException($"API error. Status code: {result.Error.Code}. Info: {result.Error.Info}");
                    }
                    return quotes;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpServiceException(ex.Message, ex);
            }
        }
    }
}
