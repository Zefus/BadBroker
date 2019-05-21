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
                List<OutputDTO> resultsForSource = new List<OutputDTO>();

                foreach (string source in sources)
                {
                    int index = 0;
                    int lastElement = quotesDTO.Count;
                    List<OutputDTO> revenues = new List<OutputDTO>();

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
                            revenues.Add(outputDTO);
                        }
                        index++;
                    }
                    OutputDTO resultForSource = revenues.Find(r => r.Revenue == revenues.Max(rev => rev.Revenue));
                    resultsForSource.Add(resultForSource);
                }
                OutputDTO result = resultsForSource.Find(r => r.Revenue == resultsForSource.Max(rev => rev.Revenue));

                return result;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }
    }
}
