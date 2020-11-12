namespace BB.DAL.Entities
{
    public class CheckingBranch
    {
        public int CheckBranchId { get; set; }
        public decimal Balance { get; set; }
        
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}