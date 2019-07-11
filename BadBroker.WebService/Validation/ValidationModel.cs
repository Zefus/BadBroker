using System;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Services;

namespace BadBroker.WebService.Validation
{
    public class ValidationModel
    {
        public bool Validate(InputDTO inputDTO)
        {
            StringToDateParser stringToDateParser = new StringToDateParser();

            DateTime startDate = stringToDateParser.Parse(inputDTO.StartDate);
            DateTime endDate = stringToDateParser.Parse(inputDTO.EndDate);

            if (inputDTO == null)
                return false;

            if (startDate == null)
                return false;

            if (endDate == null)
                return false;

            if (startDate > endDate)
                return false;

            if (startDate < new DateTime(2000, 1, 1))
                return false;

            if (startDate > DateTime.Now)
                return false;

            if (endDate < new DateTime(2000, 1, 1))
                return false;

            if (endDate > DateTime.Now)
                return false;

            return true;
        }
    }
}
