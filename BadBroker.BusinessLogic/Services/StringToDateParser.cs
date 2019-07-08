using System;

namespace BadBroker.BusinessLogic.Services
{
    public class StringToDateParser
    {
        public DateTime Parse(string date)
        {
            if (date == null || date == "")
                throw new ArgumentNullException($"Patameter {nameof(date)} cannot be null or empty", nameof(date));

            string[] numbers = date.Split('.');

            int day = Convert.ToInt32(numbers[0]);
            int month = Convert.ToInt32(numbers[1]);
            int year = Convert.ToInt32(numbers[2]);

            return new DateTime(year, month, day);
        }
    }
}
