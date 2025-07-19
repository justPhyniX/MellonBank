using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MellonBank.Models.ViewModels
{
    public class BankAccountViewModel
    {
        [Required(ErrorMessage = "Required Field")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "AFM must be exactly 9 digits.")]
        public string UserAFM { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public decimal BalanceEuro { get; set; }

        [Required(ErrorMessage = "Required Field")]
        public string AccountNumber { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public string Branch { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public string AccountType { get; set; } = null!;
    }
}
