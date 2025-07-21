using MellonBank.Models;

namespace MellonBank.Repositories
{
    public interface IUserRepository
    {
        Task<string?> UserNameToUserAFM(string userName);
        Task<List<BankAccount>> ListBankAccounts(string ownerUserName);
        Task<bool> AccountBelongsToUser(string accountNumber, string loggedInUserName);
        //Task<List<decimal>> CheckBalance();
        //Task AddMoneyToMyBankAccount(decimal amount);
        //Task<string?> SendMoney(decimal amount, string accountNumberToSend);
        //Task<BankAccount> BankAccountDetails();
        //Task ChangePassword(string oldPassword, string newPassword);
    }
}
