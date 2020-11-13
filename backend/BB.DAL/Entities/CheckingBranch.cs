namespace BB.DAL.Entities
{
    public class CheckingBranch
    {
        public int CheckingBranchId { get; set; }
        public decimal Balance { get; set; }
        
        public Card Card { get; set; }
    }
}