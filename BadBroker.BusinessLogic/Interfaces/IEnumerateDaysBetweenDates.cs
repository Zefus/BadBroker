using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IEnumerateDaysBetweenDates
    {
        List<DateTime> ExecuteAsync(DateTime startDate, DateTime endDate);
    }
}
