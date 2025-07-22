using MellonBank.Data;
using MellonBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
            var account = await _db.BankAccounts
                .FirstOrDefaultAsync(x => x.UserAFM == userAFM);
            if(account != null && account.UserAFM == userAFM)
                return true;

            return false;
        }

        public async Task<List<decimal>> CheckBalance(string accountNumber)
        {
            var account = await _db.BankAccounts
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            decimal balanceEUR = account.BalanceEuro;

            List<decimal> balances = [balanceEUR];

            decimal balanceUSD;

            using var httpClient = new HttpClient();
            var responseStream = await httpClient.GetStreamAsync(
                "https://data.fixer.io/api/latest?access_key=b02decc78401f7a2ba9a652672309cf6&base=EUR&symbols=USD"
                );
            var jsonDoc = await JsonDocument.ParseAsync(responseStream);
            if (jsonDoc.RootElement.TryGetProperty("rates", out var rates) && 
                rates.TryGetProperty("USD", out var usd) && 
                usd.TryGetDecimal(out var usdRate))
            {
                balanceUSD = balanceEUR * usdRate;
                balances.Add(balanceUSD);
            }

            return balances;
        }
    }
}
