using MellonBank.Models;

namespace MellonBank.Repositories
{
    public interface IUserRepository
    {
        Task<string?> UserNameToUserAFM(string userName);
        Task<List<BankAccount>> ListBankAccounts(string ownerUserName);
        Task<bool> AccountBelongsToUser(string accountNumber, string loggedInUserName);
        Task<List<decimal>> CheckBalance(string accountNumber);
        Task AddMoneyToMyBankAccount(string accountNumber, decimal amount);
        Task<string?> SendMoney(string accountNumberSender, decimal amount, string accountNumberReceiver);
        Task<BankAccount> BankAccountDetails(string accountNumber);
    }
}
