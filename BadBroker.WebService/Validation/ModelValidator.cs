using System;
using BadBroker.BusinessLogic.ModelsDTO;
using BadBroker.BusinessLogic.Interfaces;

namespace BadBroker.WebService.Validation
{
    public class ModelValidator : IModelValidator
    {
        private IStringToDateParser _stringToDateParser;

        public ModelValidator(IStringToDateParser stringToDateParser)
        {
            _stringToDateParser = stringToDateParser;
        }

        public bool Validate(InputDTO inputDTO)
        {
            DateTime startDate = _stringToDateParser.Parse(inputDTO.StartDate);
            DateTime endDate = _stringToDateParser.Parse(inputDTO.EndDate);
            decimal score = inputDTO.Score;

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

            if (score <= 0)
                return false;

            if ((endDate.Month - startDate.Month) > 2)
                return false;

            return true;
        }
    }
}
