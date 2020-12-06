using System.ComponentModel.DataAnnotations;

namespace BB.Common.Dto.Card
{
    public class CardCredentialsDto
    {
        [StringLength(4, MinimumLength = 4)]
        [RegularExpression("\\d*")]
        public string Number { get; set; }
        
        [RegularExpression("(?=)")]
        public string Pin { get; set; }
        
        public int UserId { get; set; }

        public void Deconstruct(out string number, out string pin)
        {
            number = Number;
            pin = Pin;
        }
    }
}