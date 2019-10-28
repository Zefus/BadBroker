using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.Extensions;
using BadBroker.BusinessLogic.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BadBroker.BusinessLogic.Services
{
    public class ExchangeRatesApiClient : IExternalServiceClient
    {
        private readonly IOptions<Config> _config;

        public ExchangeRatesApiClient(IOptions<Config> config)
        {
            _config = config;
        }

        /// <summary>
        /// Method return data about currency rates from apilayer.net by API service.
        /// </summary>
        /// <param name="dates">Dates on which currency data will be received</param>
        /// <returns>Return the collection of currency data</returns>
        public async Task<IEnumerable<RatesDTO>> GetCurrencyRatesAsync(IEnumerable<DateTime> dates)
        {
            string appId = _config.Value.AppId;
            string baseCurrency = _config.Value.Base;
            string symbols = _config.Value.Symbols;
            List<RatesDTO> rates = new List<RatesDTO>();
            HttpResponseMessage response;
            string responseBody = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://openexchangerates.org/api/");

                foreach (DateTime date in dates)
                {
                    try
                    {
                        string apiFormattedDate = date.ToApiStringFormat();
                        string url = $"historical/{apiFormattedDate}.json?app_id={appId}&base=USD&symbols=EUR,GBP,RUB,JPY";
                        response = await client.GetAsync(url);
                        responseBody = await response.Content.ReadAsStringAsync();
                        response.EnsureSuccessStatusCode();
                        RatesDTO result = JsonConvert.DeserializeObject<RatesDTO>(responseBody);
                        result.Date = date;
                        rates.Add(result);
                    }
                    catch (HttpRequestException ex)
                    {
                        ErrorModelDTO errorModelDTO = JsonConvert.DeserializeObject<ErrorModelDTO>(responseBody);
                        throw new HttpServiceException($"API error. Status code: {errorModelDTO.Status}. Message: {errorModelDTO.Message}. " +
                            $"\nDescription: {errorModelDTO.Description}", ex);
                    }
                }
                return rates;
            }
        }
    }
}
