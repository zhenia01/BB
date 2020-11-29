using System;

namespace BB.DAL.Entities
{
    public class CreditBranch
    {
        public int CreditBranchId { get; set; }
        public decimal Available { get; set; }
        public decimal Balance { get; set; }
        public double Interest { get; set; }
        public DateTime? WithdrawTime { get; set; }
        public decimal? Debt { get; set; }
        
        public Card Card { get; set; }
    }
}