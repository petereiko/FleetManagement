using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.UI.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense(FuelExpenseDto model)
        {
            return View();
        }
    }
}
