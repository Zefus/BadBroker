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
            //Определение всех доступных валют
            var currencies = new List<string> { "RUB", "EUR", "GBP", "JPY" };

            //Выделение памяти под коллекцию, где будут храниться максимальное значение прибыли для каждой валюты
            var bestRevenuesForBase = new List<OutputDTO>();

            //Создание объекта предназначенного для вычисления максимальной прибыли для выбранной валюты
            BestCaseSearcher bcs = new BestCaseSearcher();

            //Вычисление максимальной пибыли для каждой валюты и помещении их в коллекцию
            foreach (var curr in currencies)
            {               
                bestRevenuesForBase.Add(bcs.SearchBestCase(inputDTO, curr));
            }

            //Вычисление наибольшей прибыли из всех полученных для каждой валюты
            var result = bestRevenuesForBase.Find(b => b.Score == bestRevenuesForBase.Max(c => c.Score));

            //Возварщение результата функции
            return result;
        }
    }
}
