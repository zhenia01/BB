namespace BB.Common.Dto
{
    public record CardDto
    {
        public int CardId { get; init; }
        
        public string Pin { get; init; }
        
        public string Number { get; init; }
        
        public bool IsBlocked { get; init; }
    }
}