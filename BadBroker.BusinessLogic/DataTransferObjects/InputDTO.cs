using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBroker.BusinessLogic.DataTransferObjects
{
    public class InputDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<RatesDTO> AllRates { get; set; }
        public decimal Score { get; set; } = 100;
    }
}
