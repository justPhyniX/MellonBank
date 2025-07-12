using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(9)]
        public string AFM { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public UserRole Role { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
