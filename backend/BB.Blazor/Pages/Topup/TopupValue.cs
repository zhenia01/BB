using System;
using System.ComponentModel.DataAnnotations;

namespace BB.Blazor.Pages.Topup
{
    public class TopupValue
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Value { get; set; }
    }
}