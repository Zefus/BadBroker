using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadBroker.WebApi.Errors
{
    public class Error
    {
        public string Title { get; set; }
        public int Status { get; set; }

        public Error(string title, int status)
        {
            Title = title;
            Status = status;
        }
    }
}
