using System;
using System.Collections.Generic;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Services
{
    public class BestCaseSearcher : IBestCaseSearcher
    {
        /// <summary>
        /// Method of calculating the greatest benefits.
        /// </summary>
        /// <param name="ratesDTO">Currency rates</param>
        /// <returns>Greatest revenue</returns>
        public OutputDTO SearchBestCase(IList<RatesDTO> ratesDTO, decimal score)
        {
            if (ratesDTO == null)
                throw new ArgumentNullException($"Argument {nameof(ratesDTO)} is null. Method: SearchBestCase");

            List<string> sources = new List<string> { "RUB", "EUR", "GBP", "JPY" };
            OutputDTO result = new OutputDTO();
            foreach (string source in sources)
            {
                int index = 0;
                int lastElement = ratesDTO.Count;
                while (index != lastElement)
                {
                    for (int i = index; i < lastElement; i++)
                    {
                        DateTime buyDate = ratesDTO[index].Date;
                        DateTime sellDate = ratesDTO[i].Date;
                        decimal revenue = ratesDTO[index].Rates[source] * score
                            / ratesDTO[i].Rates[source] - (ratesDTO[i].Date.Subtract(ratesDTO[index].Date).Days);
                        decimal benefit = revenue - score;
                        OutputDTO outputDTO = new OutputDTO(buyDate, sellDate, source, benefit, revenue);
                        if (outputDTO.Revenue > result.Revenue)
                            result = outputDTO;
                    }
                    index++;
                }
            }
            return result;
        }
    }
}
