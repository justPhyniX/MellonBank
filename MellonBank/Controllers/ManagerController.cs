using MellonBank.Models;
using MellonBank.Models.ViewModels;
using MellonBank.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        //ADD USER
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel user)
        {
            if(!ModelState.IsValid)
                return View(user);
            await _managerRepository.AddUser(user);
            return RedirectToAction("ViewUser", new { afm = user.AFM });
        }

        //ADD BANK ACCOUNT
        [HttpGet]
        public IActionResult AddBankAccount()
        {
            if (TempData["NotFoundAFM"] != null)
                ViewBag.NotFoundAFM = TempData["NotFoundAFM"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBankAccount(BankAccountViewModel bankAccount)
        {
            if (!ModelState.IsValid)
                return View(bankAccount);

            var result = await _managerRepository.AddBankAccount(bankAccount);
            if (result)
            {
                ModelState.Clear();
                ViewBag.SuccessMessage = "Bank account created successfully!";
                return View(new BankAccountViewModel());
            }
            else
            {
                TempData["NotFoundAFM"] = bankAccount.UserAFM;
                return RedirectToAction("AddBankAccount");
            }
        }

        //VIEW USER
        [HttpGet]
        public async Task<IActionResult> ViewUser(string AFM)
        {
            var user = await _managerRepository.ViewUser(AFM);
            if (user == null)
            {
                ViewBag.NotFoundAFM = AFM;
                return View();
            }
            return View(user);
        }

        //LIST USERS
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var users = await _managerRepository.ListUsers();
            return View(users);
        }

        //DELETE BANK ACCOUNT
        [HttpGet]
        public IActionResult DeleteBankAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBankAccount(string accountNumber)
        {
            if(accountNumber == null)
                return RedirectToAction("DeleteBankAccount");

            var result = await _managerRepository.DeleteBankAccount(accountNumber);
            if (result)
            {
                ViewBag.SuccessMessage = "Bank account deleted successfully!";
                return View();
            }
            else
            {
                ViewBag.NotFoundAccountNumber = $"No bank account found to delete with account number: {accountNumber}";
                return View();
            }
        }

        //DELETE USER
        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string AFM)
        {
            if (AFM == null)
                return View();

            var result = await _managerRepository.DeleteUser(AFM);
            if(result)
                return RedirectToAction("ListUsers");
            else
            {
                ViewBag.NotFoundAFM = $"No user found to delete with AFM: {AFM}";
                return View();
            }
        }

        //EDIT USER
        [HttpGet]
        public IActionResult EditUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel userToEdit, string searchAFM)
        {
            if (searchAFM == null)
                return View();

            if (ModelState.IsValid && await _managerRepository.EditUser(userToEdit, searchAFM))
                return RedirectToAction("ViewUser", new { afm = userToEdit.AFM });
            else
            {
                ViewBag.NotFoundAFM = searchAFM;
                return RedirectToAction("EditUser");
            }
        }

        //EDIT BANK ACCOUNT
        [HttpGet]
        public IActionResult EditBankAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditBankAccount(BankAccountViewModel bankAccountToEdit, string searchAccountNumber)
        {
            if(searchAccountNumber == null)
                return View();

            if (ModelState.IsValid && await _managerRepository.EditBankAccount(bankAccountToEdit, searchAccountNumber))
            {
                ViewBag.SuccessfulEdit = $"Successfully edited bank account with number: {searchAccountNumber}";
                return View();
            }
            else
            {
                ViewBag.NotFoundAccountNumber = $"No bank account found to edit with number: {searchAccountNumber}";
                return View();
            }
        }
    }
}
