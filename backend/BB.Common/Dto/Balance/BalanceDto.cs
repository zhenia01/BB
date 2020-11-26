namespace BB.Common.Dto.Balance
{
    public record BalanceDto
    {
        public decimal CheckingBalance { get; init; }
        public decimal? CreditBalance { get; init; }
    }
}