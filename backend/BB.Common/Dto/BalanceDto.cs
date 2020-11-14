using System;

namespace BB.Common.Dto
{
    public record BalanceDto
    {
        public decimal CheckingBalance { get; init; }
        public decimal? CreditBalance { get; init; }
    }
}