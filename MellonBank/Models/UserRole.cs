using Microsoft.AspNetCore.Identity;

namespace MellonBank.Models
{
    public class UserRole : IdentityRole
    {
        public int Id { get; set; }

        public string Role {  get; set; }

        public ICollection<User> Users { get; set; }
    }
}
