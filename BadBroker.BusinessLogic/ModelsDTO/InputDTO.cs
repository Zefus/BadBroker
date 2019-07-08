using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class InputDTO
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public InputDTO() { }

        public InputDTO(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
