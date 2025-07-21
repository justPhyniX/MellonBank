using MellonBank.Data;
using MellonBank.Mapper;
using MellonBank.Models;
using MellonBank.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MellonBank.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public ManagerRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddUser(UserViewModel user)
        {
            var newUser = UserMapper.MapUserViewModelToUser(user);
            await _userManager.CreateAsync(newUser, user.Password);
            await _userManager.AddToRoleAsync(newUser, "User");
        }

        public async Task<bool> AddBankAccount(BankAccountViewModel bankAccount)
        {
            var accountOwner = await _db.Users.FirstOrDefaultAsync(x => x.AFM == bankAccount.UserAFM);
            if(accountOwner != null)
            {
                var newBankAccount = BankAccountMapper.MapBankAccountViewModelToBankAccount(bankAccount);
                accountOwner.BankAccounts.Add(newBankAccount);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteBankAccount(string accountNumber)
        {
            var account = await _db.BankAccounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if(account == null)
                return false;

            _db.BankAccounts.Remove(account);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(string AFM)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.AFM == AFM);
            if (user == null)
                return false;

            var accounts = _db.BankAccounts.Where(x => x.UserAFM == AFM);       //also deletes user's accounts 
            _db.BankAccounts.RemoveRange(accounts);

            await _userManager.DeleteAsync(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditBankAccount(BankAccountViewModel newAccount, string accountNumber)
        {
            var account = await _db.BankAccounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if(account == null)
                return false;
            else
            {
                account.BalanceEuro = newAccount.BalanceEuro;
                account.AccountNumber = newAccount.AccountNumber;
                account.Branch = newAccount.Branch;
                account.AccountType = newAccount.AccountType;
                _db.BankAccounts.Update(account);
                await _db.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> EditUser(UserViewModel newUser, string searchAFM)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.AFM == searchAFM);
            if(user == null)
                return false;
            else
            {
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Address = newUser.Address;
                user.AFM = newUser.AFM;
                user.UserName = newUser.UserName;
                user.NormalizedUserName = newUser.UserName.ToUpper();
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newUser.Password);
                string emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newUser.Email);
                await _userManager.ChangeEmailAsync(user, newUser.Email, emailToken);
                await _userManager.ChangePhoneNumberAsync(user, newUser.PhoneNumber, newUser.Email);

                return true;
            }
        }

        public async Task<List<User>> ListUsers()
        {
            var users = await _db.Users.ToListAsync();
            var usersInRole = new List<User>();

            foreach (var user in users)
            {
                if(await _userManager.IsInRoleAsync(user, "User"))
                    usersInRole.Add(user);
            }

            return usersInRole;
        }

        public async Task<User> ViewUser(string AFM)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.AFM == AFM);
        }
    }
}
