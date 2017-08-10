using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.DataTransferObjects;

namespace BadBroker.BusinessLogic.BusinessModels
{
    public class BestCaseSearcher
    {
        private List<OutputDTO> _revenues;

        public OutputDTO SearchBestCase(InputDTO inputDTO, string currency)
        {
            int index = 0;
            int lastElement = inputDTO.AllRates.Count();
            _revenues = new List<OutputDTO>();

            while (index != lastElement)
            {
                for (int i = index; i < lastElement; i++)
                {
                    var buyDate = inputDTO.AllRates[index].Date;
                    var sellDate = inputDTO.AllRates[i].Date;
                    var revenue = inputDTO.AllRates[index].Rates[currency] * inputDTO.Score
                        / inputDTO.AllRates[i].Rates[currency] - (inputDTO.AllRates[i].Date.Subtract(inputDTO.AllRates[index].Date).Days);
                    var benefit = revenue - inputDTO.Score;
                    OutputDTO outputDTO = new OutputDTO(buyDate, sellDate, currency, benefit, revenue);
                    _revenues.Add(outputDTO);
                }
                index++;
            }
            
            var result = _revenues.Find(r => r.Score == _revenues.Max(rev => rev.Score));

            return result;
        }
    }
}
