using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            if (startDate == null || endDate == null || startDate < endDate)
            {
                return false;
            }

            return true;
        }
    }
}
