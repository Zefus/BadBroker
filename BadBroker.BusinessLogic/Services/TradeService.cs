using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.DataTransferObjects;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.BusinessModels;

namespace BadBroker.BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        public OutputDTO MakeTrade(InputDTO inputDTO)
        {
            var currencies = new List<string> { "RUB", "EUR", "GBP", "JPY" };

            var bestRevenuesForBase = new List<OutputDTO>();

            BestCaseSearcher bcs = new BestCaseSearcher();

            foreach (var curr in currencies)
            {               
                bestRevenuesForBase.Add(bcs.SearchBestCase(inputDTO, curr));
            }

            var result = bestRevenuesForBase.Find(b => b.Score == bestRevenuesForBase.Max(c => c.Score));

            return result;
        }
    }
}
