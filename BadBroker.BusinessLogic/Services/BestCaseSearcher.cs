using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Services
{
    public class BestCaseSearcher : IBestCaseSearcher
    {
        public OutputDTO SearchBestCase(IList<QuotesDTO> quotesDTO)
        {
            try
            {
                decimal score = 100;
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
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
