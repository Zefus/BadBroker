using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IExternalServiceClient
    {
        Task<IEnumerable<RatesDTO>> GetCurrencyRatesAsync(IEnumerable<DateTime> date);
    }
}
