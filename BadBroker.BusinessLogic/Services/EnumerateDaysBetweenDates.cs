using System;
using System.Linq;
using System.Collections.Generic;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.BusinessLogic.Services
{
    public class EnumerateDaysBetweenDates : IEnumerateDaysBetweenDates
    {
        public List<string> ExecuteAsync(DateTime startDate, DateTime endDate)
        {
            List<string> dates = new List<string>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                string day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
                string month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();

                dates.Add($"{date.Year}-{month}-{day}");
            }

            return dates;
        }
    }
}
