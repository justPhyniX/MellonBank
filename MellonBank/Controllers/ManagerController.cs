using MellonBank.Models.ViewModels;
using MellonBank.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> AddBankAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBankAccount(BankAccountViewModel bankAccount)
        {
            if (!ModelState.IsValid)
                return View(bankAccount);
            await _managerRepository.AddBankAccount(bankAccount);
            ModelState.Clear();
            return View(new BankAccountViewModel());
        }
    }
}
