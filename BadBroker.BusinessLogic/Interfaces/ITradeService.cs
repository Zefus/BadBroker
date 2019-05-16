using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface ITradeService
    {
        OutputDTO MakeTrade(InputDTO inputDTO);
    }
}
