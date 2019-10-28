using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class ErrorModelDTO
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        public ErrorModelDTO(int status, string message, string description)
        {
            Status = status;
            Message = message;
            Description = description;
        }
    }
}
