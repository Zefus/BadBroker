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
        /// <param name="quotesDTO">Currency rates</param>
        /// <returns>Greatest revenue</returns>
        public OutputDTO SearchBestCase(IList<QuotesDTO> quotesDTO, decimal score)
        {
            if (quotesDTO == null)
                throw new ArgumentNullException($"Argument {nameof(quotesDTO)} is null. Method: SearchBestCase");
            //decimal score = 100;
            List<string> sources = new List<string> { "RUB", "EUR", "GBP", "JPY" };
            OutputDTO result = new OutputDTO();
            foreach (string source in sources)
            {
                int index = 0;
                int lastElement = quotesDTO.Count;
                while (index != lastElement)
                {
                    for (int i = index; i < lastElement; i++)
                    {
                        DateTime buyDate = quotesDTO[index].Date;
                        DateTime sellDate = quotesDTO[i].Date;
                        decimal revenue = quotesDTO[index].Quotes[$"USD{source}"] * score
                            / quotesDTO[i].Quotes[$"USD{source}"] - (quotesDTO[i].Date.Subtract(quotesDTO[index].Date).Days);
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
