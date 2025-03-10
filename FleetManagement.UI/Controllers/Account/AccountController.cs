using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.UI.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
