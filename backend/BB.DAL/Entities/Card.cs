using Microsoft.EntityFrameworkCore;

namespace BB.DAL.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string Pin { get; set; }
        public bool IsBlocked { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public DepositBranch DepositBranch { get; set; }
        public DepositBranch CreditBranch { get; set; }
        public DepositBranch CheckingBranch { get; set; }

    }
}