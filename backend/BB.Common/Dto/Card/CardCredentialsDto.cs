using System.ComponentModel.DataAnnotations;

namespace BB.Common.Dto.Card
{
    public class CardCredentialsDto
    {
        [StringLength(4, MinimumLength = 4)]
        public string Number { get; set; }
        
        public string Pin { get; set; }
        
        public int UserId { get; set; }

        public void Deconstruct(out string number, out string pin, out int userId)
        {
            number = Number;
            pin = Pin;
            userId = UserId;
        }
    }
}