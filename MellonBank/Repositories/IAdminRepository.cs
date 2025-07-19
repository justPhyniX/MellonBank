using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Repositories
{
    public interface IAdminRepository
    {
        Task AddManager(UserViewModel user);
        Task<User> GetManagerByUserNameAsync(string userName);
    }
}
