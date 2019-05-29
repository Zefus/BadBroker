using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IStringToDateParser
    {
        DateTime Parse(string date);
    }
}
