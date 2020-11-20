namespace BB.Common.Dto.Card
{
    public record CardCredentialsDto
    {
        public string Number { get; init; }
        public string Pin { get; init; }

        public void Deconstruct(out string number, out string pin)
        {
            number = Number;
            pin = Pin;
        }
    }
}