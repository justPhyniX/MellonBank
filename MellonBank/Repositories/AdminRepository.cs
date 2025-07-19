using MellonBank.Mapper;
using MellonBank.Models;
using MellonBank.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MellonBank.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<User> _userManager;

        public AdminRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddManager(UserViewModel manager)
        {
            var user = UserMapper.MapUserViewModelToUser(manager);
            await _userManager.CreateAsync(user, manager.Password);
            await _userManager.AddToRoleAsync(user, "Manager");
        }

        public async Task<User> GetManagerByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
    }
}
