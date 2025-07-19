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

        //ADD BANK ACCOUNT
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

        //ADD USER
        [HttpGet]
        public async Task<ActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserViewModel user)
        {
            if(!ModelState.IsValid)
                return View(user);
            await _managerRepository.AddUser(user);
            return RedirectToAction("ViewUser", new { afm = user.AFM });
        }

        //VIEW USER
        [HttpGet]
        public async Task<ActionResult> ViewUser(string AFM)
        {
            var user = await _managerRepository.ViewUser(AFM);
            if (user == null)
            {
                ViewBag.NotFoundAFM = AFM;
                return View(null);
            }
            return View(user);
        }

        //LIST USERS
        [HttpGet]
        public async Task<ActionResult> ListUsers()
        {
            var users = await _managerRepository.ListUsers();
            return View(users);
        }

        //DELETE USER
        [HttpGet]
        public async Task<ActionResult> DeleteUser()
        {
            if (TempData["NotFoundAFM"] != null)
            {
                ViewBag.NotFoundAFM = TempData["NotFoundAFM"];
            }
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string AFM)
        {
            if (AFM == null)
                return RedirectToAction("DeleteUser");

            var result = await _managerRepository.DeleteUser(AFM);
            if(result)
            {
                return RedirectToAction("ListUsers");
            }
            else
            {
                TempData["NotFoundAFM"] = AFM;
                return RedirectToAction("DeleteUser");
            }
        }

        //EDIT USER
        [HttpGet]
        public async Task<ActionResult> EditUser()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> EditUser(string AFM)
        //{
        //    if (AFM == null)
        //        return RedirectToAction("EditUser");


        //}
    }
}
