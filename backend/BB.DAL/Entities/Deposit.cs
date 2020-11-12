namespace BB.DAL.Entities
{
    public class Deposit
    {
        public int DepositId { get; set; }
        public int DepSum { get; set; }
        public double Percent { get; set; }
        public string CurrencyOfDeposit { get; set; }
        public int Term { get; set; }
        public bool PaymentsToDeposit { get; set; }
        public bool CanBeTerminated { get; set; }
        
        public DepositBranch DepBranchId { get; set; }
        public DepositBranch DepositBranch { get; set; }
    }
}