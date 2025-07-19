using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MellonBank.Models
{
    public class BankAccount
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserAFM { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public decimal BalanceEuro { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public string AccountNumber { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public string AccountType { get; set; }
    }
}
