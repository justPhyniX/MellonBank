using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MellonBank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(9)]
        public string UserAFM { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceEuro { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public string AccountType { get; set; }
    }
}
