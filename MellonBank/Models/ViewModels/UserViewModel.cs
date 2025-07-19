using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Required Field")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        [EmailAddress(ErrorMessage = "Wrong Email Format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "AFM must be exactly 9 digits")]
        public string AFM { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Required Field")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).+$",
        ErrorMessage = "Must have at least one uppercase, one number and one symbol")]
        public string Password { get; set; } = null!;
    }
}
