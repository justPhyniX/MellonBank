using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Repositories
{
    public interface IManagerRepository
    {
        Task AddUser(UserViewModel user);
        Task AddBankAccount(BankAccountViewModel bankAccount);
        Task<bool> DeleteUser(string AFM);
        Task<bool> DeleteBankAccount(string accountNumber);
        Task<bool> EditUser(string AFM);
        Task<bool> EditBankAccount(string accountNumber);
        Task<User> ViewUser(string AFM);
        Task<List<User>> ListUsers();
    }
}
