using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBroker.BusinessLogic.DataTransferObjects
{
    public class OutputDTO
    {
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public string Currency { get; set; }
        public decimal Benefit { get; set; }
        public decimal Score { get; set; }

        public OutputDTO() { }
        public OutputDTO(DateTime buyDate, DateTime sellDate, string currency, decimal benefit, decimal score)
        {
            BuyDate = buyDate;
            SellDate = sellDate;
            Currency = currency;
            Benefit = benefit;
            Score = score;
        }
    }
}
