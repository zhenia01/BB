﻿namespace BB.DAL.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string Pin { get; set; }
        public string Number { get; set; }
        public bool IsBlocked { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public DepositBranch DepositBranch { get; set; }
        public int? DepositBranchId { get; set; }
        
        public CreditBranch CreditBranch { get; set; }
        public int? CreditBranchId { get; set; }
        
        public CheckingBranch CheckingBranch { get; set; }
        public int CheckingBranchId { get; set; }

    }
}