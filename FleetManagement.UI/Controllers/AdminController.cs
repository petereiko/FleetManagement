using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
