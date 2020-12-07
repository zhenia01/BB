namespace BB.Common.Dto.DepositDto
{
    public record DepositDto
    {
        public decimal DepSum { get; init; }
        public int Term { get; init; }
        public bool PaymentsToDeposit { get; init; }
        public bool CanBeTerminated { get; init; }
        public int CardId { get; init; }
    }
}