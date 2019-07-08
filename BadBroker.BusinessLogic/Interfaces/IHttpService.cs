using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IHttpService
    {
        Task<IEnumerable<QuotesDTO>> GetCurrencyRatesAsync(IEnumerable<DateTime> date);
    }
}
