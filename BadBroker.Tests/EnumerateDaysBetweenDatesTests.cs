using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BadBroker.BusinessLogic.Exceptions;
using BadBroker.BusinessLogic.Services;

namespace BadBroker.Tests
{
    public class EnumerateDaysBetweenDatesTests
    {
        [Fact]
        public void EqualsResultsDates()
        {
            // Arrange
            EnumerateDaysBetweenDates enumerateDaysBetweenDates = new EnumerateDaysBetweenDates();
            DateTime startDate = new DateTime(2019, 5, 1);
            DateTime endDate = new DateTime(2019, 5, 31);
            
            List<DateTime> dates = new List<DateTime>();

            DateTime date = new DateTime(2019, 5, 1);
            dates.Add(date);

            for (int i = 1; i < 31; i++)
            {
                dates.Add(dates[i - 1].AddDays(1));
            }

            // Act
            List<DateTime> resultDates = (List<DateTime>)enumerateDaysBetweenDates.Execute(startDate, endDate);

            // Assert
            for (int i = 0; i < 31; i++)
            {
                Assert.Equal(dates[i], resultDates[i]);
            }
        }
    }
}
