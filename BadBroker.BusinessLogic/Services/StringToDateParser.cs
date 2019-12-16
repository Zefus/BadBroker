using System;
using System.Globalization;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.BusinessLogic.Services
{
    public class StringToDateParser : IStringToDateParser
    {
        public DateTime Parse(string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime result = DateTime.ParseExact(date, "dd'.'MM'.'yyyy", provider);

            return result;
        }
    }
}
