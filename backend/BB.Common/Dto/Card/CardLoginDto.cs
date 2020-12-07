using System.ComponentModel.DataAnnotations;

namespace BB.Common.Dto.Card
{
    public class CardLoginDto
    {
        public string Number { get; set; }
        
        public string Pin { get; set; }
        
        public void Deconstruct(out string number, out string pin)
        {
            number = Number;
            pin = Pin;
        }
    }
}