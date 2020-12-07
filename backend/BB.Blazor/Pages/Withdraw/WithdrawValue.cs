using System.ComponentModel.DataAnnotations;

namespace BB.Blazor.Pages.Withdraw
{
    public class WithdrawValue
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Value { get; set; }
    }
}