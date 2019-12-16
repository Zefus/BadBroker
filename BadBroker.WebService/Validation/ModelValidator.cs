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

        public ValidationResponse Validate(InputDTO inputDTO)
        {
            DateTime startDate = _stringToDateParser.Parse(inputDTO.StartDate);
            DateTime endDate = _stringToDateParser.Parse(inputDTO.EndDate);
            decimal score = inputDTO.Score;

            ValidationResponse response = new ValidationResponse();
            response.IsSuccsess = false;

            if (inputDTO == null)
            {
                response.Message = "Input data is empty";
                return response;
            }                

            if (startDate == null)
            {
                response.Message = "Start date is empty";
                return response;
            }                

            if (endDate == null)
            {
                response.Message = "End date is empty";
                return response;
            }

            if (startDate > endDate)
            {
                response.Message = "Start date less then end date";
                return response;
            }

            if (startDate < new DateTime(2000, 1, 1))
            {
                response.Message = "Start date less then minimum value";
                return response;
            }                

            if (startDate > DateTime.Now)
            {
                response.Message = "Start date more then current date";
                return response;
            }                

            if (endDate < new DateTime(2000, 1, 1))
            {
                response.Message = "End date less then minimum value";
                return response;
            }                

            if (endDate > DateTime.Now)
            {
                response.Message = "End date more then current date";
                return response;
            }                

            if (score <= 0)
            {
                response.Message = "Score less or equal zero";
                return response;
            }                

            if ((endDate.Month - startDate.Month) > 2)
            {
                response.Message = "Selected time span more then 2 months";
                return response;
            }               

            response.IsSuccsess = true;

            return response;
        }
    }
}
