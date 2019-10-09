using System;
using System.Collections.Generic;
using System.Text;
using BadBroker.BusinessLogic.ModelsDTO;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IBestCaseSearcher
    {
        OutputDTO SearchBestCase(IList<RatesDTO> rates, decimal score);
    }
}
