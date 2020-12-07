using System.Collections.Generic;

namespace BB.DAL.Entities
{
    public class DepositBranch
    {
        public int DepositBranchId { get; set; }
        
        public Card Card { get; set; }
        
        public List<Deposit> Deposits { get; set; }
    }
}