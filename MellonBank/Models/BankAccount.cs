using System.ComponentModel.DataAnnotations.Schema;

namespace MellonBank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string UserAFM { get; set; } = null!;

        public User User { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceEuro { get; set; }

        public string AccountNumber { get; set; } = null!;

        public string Branch { get; set; } = null!;

        public string AccountType { get; set; } = null!;
    }
}
