using System;
using System.ComponentModel.DataAnnotations;

namespace BB.Blazor.Pages.MobileTopUp
{
    public class MobileValue
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}