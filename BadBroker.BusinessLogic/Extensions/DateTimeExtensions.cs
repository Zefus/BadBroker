using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToApiStringFormat(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd");
        }
    }
}
