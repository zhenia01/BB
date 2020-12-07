namespace BB.Common.Dto.DepositDto
{
    public class CreditBalanceDto
    {
        public decimal Available { get; set; }
        public decimal Balance { get; set; }
        public decimal? Debt { get; set; }
    }
}