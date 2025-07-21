using MellonBank.Data;
using MellonBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<string?> UserNameToUserAFM(string userName)
        {
            return await _db.Users
                .Where(x => x.UserName == userName)
                .Select(x => x.AFM)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BankAccount>> ListBankAccounts(string ownerUserName)
        {
            var ownerAFM = await UserNameToUserAFM(ownerUserName);
            List<BankAccount> accounts = [];
            if (ownerAFM != null)
            {
                accounts = await _db.BankAccounts
                    .Where(x => x.UserAFM == ownerAFM)
                    .ToListAsync();
            }

            return accounts;
        }

        public async Task<bool> AccountBelongsToUser(string accountNumber, string loggedInUserName)
        {
            var userAFM = await UserNameToUserAFM(loggedInUserName);
            var account = await _db.BankAccounts.FirstOrDefaultAsync(x => x.UserAFM == userAFM);
            if(account != null && account.UserAFM == userAFM)
                return true;

            return false;
        }

        //public Task<List<decimal>> CheckBalance(string accountNumber)
        //{

        //}
    }
}
