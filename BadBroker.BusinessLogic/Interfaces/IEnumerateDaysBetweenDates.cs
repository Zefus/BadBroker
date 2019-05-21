using System;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IEnumerateDaysBetweenDates
    {
        IEnumerable<DateTime> Execute(DateTime startDate, DateTime endDate);
    }
}
