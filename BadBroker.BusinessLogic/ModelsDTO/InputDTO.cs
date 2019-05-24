using System;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class InputDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public InputDTO() { }

        public InputDTO(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
