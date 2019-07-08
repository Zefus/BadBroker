using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToApiStringFormat(this DateTime date)
        {
            string day = date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            string month = date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();

            return $"{date.Year}-{month}-{day}";
        }
    }
}
