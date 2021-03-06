﻿namespace BB.DAL.Entities
{
    public class Deposit
    {
        public int DepositId { get; set; }
        public decimal DepSum { get; set; }
        public double Percent { get; set; }

        public int Term { get; set; }
        public bool PaymentsToDeposit { get; set; }
        public bool CanBeTerminated { get; set; }
        
        public int DepositBranchId { get; set; }
        public DepositBranch DepositBranch { get; set; }
    }
}
