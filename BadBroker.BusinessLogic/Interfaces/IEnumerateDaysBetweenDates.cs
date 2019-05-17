using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IEnumerateDaysBetweenDates
    {
        List<string> ExecuteAsync(DateTime startDate, DateTime endDate);
    }
}
