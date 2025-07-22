using MellonBank.Models;
using MellonBank.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //SELECT BANK ACCOUNT
        [HttpGet]
        public async Task<IActionResult> SelectBankAccount()
        {
            var accounts = await _userRepository.ListBankAccounts(User.Identity.Name);
            if (accounts.Count >= 1)
                return View(accounts);

            return View(new List<BankAccount>());
        }

        [HttpPost]
        public async Task<IActionResult> SelectBankAccount(string accountNumber)
        {
            bool isAuthorized = await _userRepository.AccountBelongsToUser(accountNumber, User.Identity.Name);
            if (!isAuthorized)
                return Unauthorized();

            HttpContext.Session.SetString("SelectedAccountNumber", accountNumber);
            return RedirectToAction("AccountActions");
        }

        //ACCOUNT ACTIONS
        [HttpGet]
        public IActionResult AccountActions()
        {
            var selectedAccountNumber = HttpContext.Session.GetString("SelectedAccountNumber");
            ViewData["SelectedAccountNumber"] = selectedAccountNumber;
            return View();
        }

        //CHECK BALANCE
        [HttpGet]
        public async Task<IActionResult> CheckBalance(string accountNumber)
        {
            var balances = await _userRepository.CheckBalance(accountNumber);
            return View(balances);
        }

        //ADD MONEY TO MY ACCOUNT
        [HttpGet]
        public IActionResult AddMoneyToMyAccount(string accountNumber)
        {
            return View(model: accountNumber);
        }

        [HttpPost]
        public async Task<IActionResult> AddMoneyToMyAccount(string accountNumber, decimal amount)
        {
            await _userRepository.AddMoneyToMyBankAccount(accountNumber, amount);
            return RedirectToAction("CheckBalance", new { accountNumber });
        }

        //Send Money
        [HttpGet]
        public IActionResult SendMoney(string accountNumber)
        {
            return View(model: accountNumber);
        }

        [HttpPost]
        public async Task<IActionResult> SendMoney(string accountNumber, decimal amount, string accountNumberReceiver)
        {
            string? errorMessage = await _userRepository.SendMoney(accountNumber, amount, accountNumberReceiver);

            if (errorMessage == null)
                ViewBag.MoneySentSuccessfully = $"{amount}€ were successfully sent to account: {accountNumberReceiver}";
            else if (errorMessage == "Receiver Not Found")
                ViewBag.ReceiverNotFound = errorMessage;
            else if (errorMessage == "Ιnsufficient Βalance")
                ViewBag.InsufficientBalance = errorMessage;
            else
                ViewBag.SomethingWentWrong = errorMessage;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BankAccountDetails(string accountNumber)
        {
            var account = await _userRepository.BankAccountDetails(accountNumber);
            var balances = await _userRepository.CheckBalance(accountNumber);
            ViewBag.BalanceUSD = balances[1];
            return View(account);
        }
    }
}
