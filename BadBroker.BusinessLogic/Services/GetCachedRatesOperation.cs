using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BadBroker.DataAccess.Models;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Services
{
    public class GetCachedRatesOperation : IGetCachedRatesOperation
    {
        private IDBService _dBService;

        public GetCachedRatesOperation(IDBService dBService)
        {
            _dBService = dBService;
        }

        /// <summary>
        /// Method that get rates data from database.
        /// </summary>
        /// <param name="cachedDates">Dates for which method returns rates data</param>
        /// <returns>Collection of RatesDTO objects</returns>
        public async Task<IEnumerable<RatesDTO>> ExecuteAsync(IEnumerable<DateTime> cachedDates)
        {
            List<RatesDTO> cachedRates = new List<RatesDTO>();

            if (cachedDates.Count() != 0)
            {
                IEnumerable<RatesData> ratesDatas = await _dBService.GetRates<RatesData>(rd => cachedDates.Contains(rd.Date), rd => rd.RatesPerDate);

                foreach (RatesData ratesData in ratesDatas)
                {
                    Dictionary<string, decimal> mappedRates = new Dictionary<string, decimal>();

                    ratesData.RatesPerDate.ForEach(rpd => mappedRates.Add(rpd.Name, rpd.Rate));

                    cachedRates.Add(new RatesDTO(ratesData.Date, mappedRates));
                }
            }

            return cachedRates;
        }
    }
}
