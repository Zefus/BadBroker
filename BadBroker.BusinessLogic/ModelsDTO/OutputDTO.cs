using System;

namespace BadBroker.BusinessLogic.ModelsDTO
{
    public class OutputDTO
    {
        public DateTime BuyDate { get; set; }
        public DateTime SellDate { get; set; }
        public string Currency { get; set; }
        public decimal Benefit { get; set; }
        public decimal Revenue { get; set; }

        public OutputDTO() { }
        public OutputDTO(DateTime buyDate, DateTime sellDate, string currency, decimal benefit, decimal revenue)
        {
            BuyDate = buyDate;
            SellDate = sellDate;
            Currency = currency;
            Benefit = benefit;
            Revenue = revenue;
        }
    }
}
