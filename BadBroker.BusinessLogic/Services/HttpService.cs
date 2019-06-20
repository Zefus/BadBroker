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
    public class HttpService : IHttpService
    {
        private const string ACCESS_KEY = "c322dc640d70be2026e7ae22dd41417c";

        /// <summary>
        /// Method return data about currency rates from apilayer.net by API service.
        /// </summary>
        /// <param name="dates">Dates on which currency data will be received</param>
        /// <returns>Return the collection of currency data</returns>
        public async Task<IEnumerable<QuotesDTO>> GetCurrencyRatesAsync(IEnumerable<DateTime> dates)
        {
            try
            {
                List<QuotesDTO> quotes = new List<QuotesDTO>();

                HttpResponseMessage response;

                using (HttpClient client = new HttpClient())
                {
                    foreach (DateTime date in dates)
                    {
                        response = await client.GetAsync($"http://apilayer.net//historical?access_key={ACCESS_KEY}&date={date.ToApiStringFormat()}&currencies=RUB,EUR,GBP,JPY&format=1");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        QuotesDTO result = JsonConvert.DeserializeObject<QuotesDTO>(responseBody);
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
