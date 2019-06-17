using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class ErrorModelDTO
    {
        public int Code { get; set; }
        public string Info { get; set; }

        public ErrorModelDTO(int code, string info)
        {
            Code = code;
            Info = info;
        }
    }
}
