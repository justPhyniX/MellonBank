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

            return RedirectToAction("AccountActions");
        }

        //ACCOUNT ACTIONS
        [HttpGet]
        public IActionResult AccountActions()
        {
            return View();
        }
    }
}
