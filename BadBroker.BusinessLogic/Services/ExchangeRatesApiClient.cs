using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BadBroker.BusinessLogic.Services
{
    public class ExchangeRatesApiClient : IExternalServiceClient
    {
        private readonly IOptions<Config> _config;
        private readonly HttpClient _httpClient;

        public ExchangeRatesApiClient(IOptions<Config> config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
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

            foreach (DateTime date in dates)
            {
                try
                {
                    string apiFormattedDate = date.ToString("yyyy'-'MM'-'dd");
                    string url = $"historical/{apiFormattedDate}.json?app_id={appId}&base={baseCurrency}&symbols={symbols}";

                    response = await _httpClient.GetAsync(url);
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

