using System;
using System.Linq;
using System.Collections.Generic;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.BusinessLogic.Services
{
    public class EnumerateDaysBetweenDates : IEnumerateDaysBetweenDates
    {
        public List<DateTime> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }
    }
}
