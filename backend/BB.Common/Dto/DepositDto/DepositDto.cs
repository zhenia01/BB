namespace BB.Common.Dto.DepositDto
{
    public class DepositDto
    {
        public decimal DepSum { get; set; }
        public int Term { get; init; }
        public bool PaymentsToDeposit { get; init; }
        public bool CanBeTerminated { get; init; }
        public int DepositBranchId { get; init; }
    }
}