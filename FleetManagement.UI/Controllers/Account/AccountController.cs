using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.UI.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Register(RegisterDto model)
        {
            return View();
        }

        public IActionResult Login(LoginDto model)
        {
            return View();
        }

        public IActionResult Forgotpassword()
        {
            return View();
        }
    }
}
