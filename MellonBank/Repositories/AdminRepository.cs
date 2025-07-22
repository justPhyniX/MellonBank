using MellonBank.Data;
using MellonBank.Mapper;
using MellonBank.Models;
using MellonBank.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public AdminRepository(UserManager<User> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
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

        public async Task<List<User>> ListManagers()
        {
            var managers = await _db.Users.ToListAsync();
            var managersInRole = new List<User>();

            foreach (var user in managers)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                    managersInRole.Add(user);
            }

            return managersInRole;
        }
    }
}
