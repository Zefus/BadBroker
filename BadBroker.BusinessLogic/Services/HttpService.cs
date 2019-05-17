using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.BusinessLogic.Services
{
    //public class HttpService : IHttpService
    //{
    //    private const string ACCESS_KEY = "c322dc640d70be2026e7ae22dd41417c";

    //    public async Task<List<QuotesDTO>> GetCurrencyRatesAsync(DateTime date)
    //    {


    //        using (HttpClient client = new HttpClient())
    //        {
    //            try
    //            {
    //                HttpResponseMessage response = await client.GetAsync($"http://apilayer.net/api/historical?{ACCESS_KEY}&date={date}");
    //            }
    //            catch (HttpRequestException e)
    //            {

    //            }
    //        }
    //    }


    //}
}
