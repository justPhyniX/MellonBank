using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Mapper
{
    public static class UserMapper
    {
        public static User MapUserViewModelToUser(UserViewModel userViewModel)
        {
            User user = new User();

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Address = userViewModel.Address;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.Email = userViewModel.Email;
            user.AFM = userViewModel.AFM;
            user.UserName = userViewModel.UserName;
            user.PasswordHash = userViewModel.Password;
            user.EmailConfirmed = true;

            return user;
        }
    }
}
