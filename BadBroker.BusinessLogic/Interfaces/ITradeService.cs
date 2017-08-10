using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadBroker.BusinessLogic.DataTransferObjects;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface ITradeService
    {
        OutputDTO MakeTrade(InputDTO inputDTO);
    }
}
