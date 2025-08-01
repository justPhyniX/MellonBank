﻿using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Repositories
{
    public interface IManagerRepository
    {
        Task AddUser(UserViewModel user);
        Task<bool> AddBankAccount(BankAccountViewModel bankAccount);
        Task<bool> DeleteUser(string AFM);
        Task<bool> DeleteBankAccount(string accountNumber);
        Task<bool> EditUser(UserViewModel newUser, string searchAFM);
        Task<bool> EditBankAccount(BankAccountViewModel newAccount, string accountNumber);
        Task<User> ViewUser(string AFM);
        Task<List<User>> ListUsers();
    }
}
