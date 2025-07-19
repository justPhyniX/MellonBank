using MellonBank.Models;
using MellonBank.Models.ViewModels;

namespace MellonBank.Mapper
{
    public class BankAccountMapper
    {
        public static BankAccount MapBankAccountViewModelToBankAccount(BankAccountViewModel bankAccountViewModel)
        {
            BankAccount bankAccount = new BankAccount();

            bankAccount.UserAFM = bankAccountViewModel.UserAFM;
            bankAccount.BalanceEuro = bankAccountViewModel.BalanceEuro;
            bankAccount.AccountNumber = bankAccountViewModel.AccountNumber;
            bankAccount.Branch = bankAccountViewModel.Branch;
            bankAccount.AccountType = bankAccountViewModel.AccountType;

            return bankAccount;
        }
    }
}
