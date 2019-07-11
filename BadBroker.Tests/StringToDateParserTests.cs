using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BadBroker.BusinessLogic.Exceptions;
using BadBroker.BusinessLogic.Services;

namespace BadBroker.Tests
{
    public class StringToDateParserTests
    {
        [Fact]
        public void DateArgumentNUllException()
        {
            // Arrange
            string nullDate = null;
            string emptyDate = "";
            string invalidDate = "01*04/2016";
            StringToDateParser stringToDateParser = new StringToDateParser();

            // Act

            // Assert
            Assert.Throws<ArgumentNullException>(() => { stringToDateParser.Parse(nullDate); });
            Assert.Throws<ArgumentNullException>(() => { stringToDateParser.Parse(emptyDate); });
            Assert.Throws<InvalidDateException>(() => { stringToDateParser.Parse(invalidDate); });
        }

        [Fact]
        public void DatesEqualsParsingDate()
        {
            // Arrange
            StringToDateParser stringToDateParser = new StringToDateParser();
            List<string> stringDates = new List<string>();
            List<DateTime> validDates = new List<DateTime>();
            List<DateTime> resultDates = new List<DateTime>();

            for (int i = 1; i < 11; i++)
            {
                stringDates.Add($"{i.ToString()}.05.2019");
            }

            DateTime date = new DateTime(2019, 05, 1);
            validDates.Add(date);
            for (int i = 1; i < 10; i++)
            {
                validDates.Add(validDates[i - 1].AddDays(1));
            }

            // Act
            foreach (string stringDate in stringDates)
            {
                resultDates.Add(stringToDateParser.Parse(stringDate));
            }

            // Assert
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(validDates[i], resultDates[i]);
            }
        }
    }
}
