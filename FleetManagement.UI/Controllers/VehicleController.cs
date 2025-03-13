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

        public IActionResult ExpenseList(FuelExpenseDto model)
        {
            // Mock data for demonstration
            var mockExpenses = new List<FuelExpenseDto>
            {
                new FuelExpenseDto { Amount = 5000, State = "Lagos", LGA = "Ikeja", FillingStation = "Total", Date = new DateOnly(2024, 3, 10) },
                new FuelExpenseDto { Amount = 7000, State = "Abuja", LGA = "Garki", FillingStation = "Mobil", Date = new DateOnly(2024, 3, 9) },
                new FuelExpenseDto { Amount = 3500, State = "Kano", LGA = "Nassarawa", FillingStation = "NNPC", Date = new DateOnly(2024, 3, 8) }
            };

            return View(mockExpenses);
        }
    }
}
