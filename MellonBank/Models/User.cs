using Microsoft.AspNetCore.Identity;

namespace MellonBank.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            BankAccounts = new List<BankAccount>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string AFM { get; set; } = null!;
        public ICollection<BankAccount> BankAccounts { get; set; } = null!;
    }
}
