using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MellonBank.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string UserAFM { get; set; }

        public User User { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceEuro { get; set; }

        public string AccountNumber { get; set; }

        public string Branch { get; set; }

        public string AccountType { get; set; }
    }
}
