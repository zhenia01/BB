using System.Collections.Generic;

namespace BB.DAL.Entities
{
    public class DepositBranch
    {
        public int DepBranchId { get; set; }
        
        public int CardId { get; set; }
        public Card Card { get; set; }
        
        public List<Deposit> Deposits { get; set; }
    }
}