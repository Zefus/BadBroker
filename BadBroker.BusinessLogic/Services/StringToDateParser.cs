using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Services
{
    public class StringToDateParser
    {
        public DateTime Parse(string date)
        {
            string[] numbers = date.Split('.');

            int day = Convert.ToInt32(numbers[0]);
            int month = Convert.ToInt32(numbers[1]);
            int year = Convert.ToInt32(numbers[2]);

            return new DateTime(year, month, day);
        }
    }
}
