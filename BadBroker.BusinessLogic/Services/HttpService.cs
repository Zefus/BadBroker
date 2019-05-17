﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;
using Newtonsoft.Json;

namespace BadBroker.BusinessLogic.Services
{
    public class HttpService : IHttpService
    {
        private const string ACCESS_KEY = "c322dc640d70be2026e7ae22dd41417c";

        public async Task<List<QuotesDTO>> GetCurrencyRatesAsync(List<string> dates)
        {
            List<QuotesDTO> quotes = new List<QuotesDTO>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    foreach (string date in dates)
                    {
                        HttpResponseMessage response = await client.GetAsync($"http://apilayer.net/api/historical?access_key={ACCESS_KEY}&date={date}currencies=RUB,EUR,GBP,JPY&format=1");
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        QuotesDTO result = JsonConvert.DeserializeObject<QuotesDTO>(responseBody);
                        quotes.Add(result);
                    }
                    return quotes;
                }
                catch (HttpRequestException e)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}