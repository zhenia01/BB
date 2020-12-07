namespace BB.Common.Dto.User
{
    public record UserDto
    {
        public int UserId { get; init; }
        
        public string FirstName { get; init; }
        
        public string LastName { get; init; }
    }
}