using System;
using BadBroker.BusinessLogic.Exceptions;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.BusinessLogic.Services
{
    public class StringToDateParser : IStringToDateParser
    {
        public DateTime Parse(string date)
        {
            if (date == null || date == "")
                throw new ArgumentNullException("date", $"Parameter {nameof(date)} cannot be null or empty");

            string[] numbers = date.Split('.');

            if (numbers.Length < 3)
                throw new InvalidDateException($"Error: date argument is not contains '.' symbol");

            int day = Convert.ToInt32(numbers[0]);
            int month = Convert.ToInt32(numbers[1]);
            int year = Convert.ToInt32(numbers[2]);

            return new DateTime(year, month, day);
        }
    }
}
