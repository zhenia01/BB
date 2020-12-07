namespace BB.Common.Dto.DepositDto
{
    public record DepositDto
    {
        public decimal DepSum { get; set; }
        public int Term { get; set; }
        public bool PaymentsToDeposit { get; set; }
        public bool CanBeTerminated { get; set; }
        public int CardId { get; set; }
    }
}