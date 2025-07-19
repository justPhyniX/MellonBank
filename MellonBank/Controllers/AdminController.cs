using MellonBank.Models.ViewModels;
using MellonBank.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public async Task<ActionResult> AddManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddManager(UserViewModel manager)
        {
            if (!ModelState.IsValid)
                return View(manager);
            await _adminRepository.AddManager(manager);
            return RedirectToAction("ViewManager", new { userName = manager.UserName });
        }

        [HttpGet]
        public async Task<ActionResult> ViewManager(string userName)
        {
            var user = await _adminRepository.GetManagerByUserNameAsync(userName);
            if (user == null)
                return NotFound();
            return View(user);
        }
    }
}
