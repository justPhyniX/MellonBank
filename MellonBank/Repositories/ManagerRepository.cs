using MellonBank.Data;
using MellonBank.Mapper;
using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _db;
        public ManagerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddBankAccount(BankAccountViewModel bankAccount)
        {
            var newBankAccount = BankAccountMapper.MapBankAccountViewModelToBankAccount(bankAccount);
            await _db.BankAccounts.AddAsync(newBankAccount);
            await _db.SaveChangesAsync();
        }

        public Task AddUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBankAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(string AFM)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditBankAccount(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUser(string AFM)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> ListUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> ViewUser(string AFM)
        {
            throw new NotImplementedException();
        }
    }
}
