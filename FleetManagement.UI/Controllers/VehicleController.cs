using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class VehicleController : Controller
    {
        private const string SessionKey = "FuelExpenses";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Expense(FuelExpenseDto model)
        {
            if (ModelState.IsValid)
            {
                var expenses = GetFuelExpenses();
                expenses.Add(model);

                // Save updated list to session
                HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(expenses));

                TempData["SuccessMessage"] = "Fuel expense added successfully!";
                return RedirectToAction("ExpenseList");
            }

            return View(model);
        }

        public IActionResult ExpenseList(int page = 1, int pageSize = 10)
        {
            var allExpenses = GetFuelExpenses(); // Retrieve all expenses
            int totalCount = allExpenses.Count();

            var paginatedExpenses = allExpenses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(paginatedExpenses);
        }


        private List<FuelExpenseDto> GetFuelExpenses()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            if (!string.IsNullOrEmpty(sessionData))
            {
                return JsonSerializer.Deserialize<List<FuelExpenseDto>>(sessionData);
            }

            // If session is empty, initialize with mock data
            var mockExpenses = new List<FuelExpenseDto>
            {
                new FuelExpenseDto { Amount = 5000, State = "Lagos", LGA = "Ikeja", FillingStation = "Total", Date = new DateOnly(2024, 3, 10) },
                new FuelExpenseDto { Amount = 7000, State = "Abuja", LGA = "Garki", FillingStation = "Mobil", Date = new DateOnly(2024, 3, 9) },
                new FuelExpenseDto { Amount = 3500, State = "Kano", LGA = "Nassarawa", FillingStation = "NNPC", Date = new DateOnly(2024, 3, 8) },
                new FuelExpenseDto { Amount = 4800, State = "Lagos", LGA = "Lekki", FillingStation = "Oando", Date = new DateOnly(2024, 3, 7) },
                new FuelExpenseDto { Amount = 6000, State = "Rivers", LGA = "Port Harcourt", FillingStation = "Total", Date = new DateOnly(2024, 3, 6) },
                new FuelExpenseDto { Amount = 8000, State = "Ogun", LGA = "Abeokuta", FillingStation = "Conoil", Date = new DateOnly(2024, 3, 5) },
                new FuelExpenseDto { Amount = 9000, State = "Kaduna", LGA = "Kaduna North", FillingStation = "NNPC", Date = new DateOnly(2024, 3, 4) },
                new FuelExpenseDto { Amount = 5500, State = "Kogi", LGA = "Lokoja", FillingStation = "Mobil", Date = new DateOnly(2024, 3, 3) },
                new FuelExpenseDto { Amount = 7200, State = "Enugu", LGA = "Enugu East", FillingStation = "Total", Date = new DateOnly(2024, 3, 2) },
                new FuelExpenseDto { Amount = 6500, State = "Benue", LGA = "Makurdi", FillingStation = "Oando", Date = new DateOnly(2024, 3, 1) },
                new FuelExpenseDto { Amount = 4800, State = "Delta", LGA = "Warri", FillingStation = "Conoil", Date = new DateOnly(2024, 2, 28) },
                new FuelExpenseDto { Amount = 5200, State = "Osun", LGA = "Osogbo", FillingStation = "Total", Date = new DateOnly(2024, 2, 27) },
                new FuelExpenseDto { Amount = 7800, State = "Ekiti", LGA = "Ado Ekiti", FillingStation = "Mobil", Date = new DateOnly(2024, 2, 26) },
                new FuelExpenseDto { Amount = 6700, State = "Edo", LGA = "Benin City", FillingStation = "NNPC", Date = new DateOnly(2024, 2, 25) },
                new FuelExpenseDto { Amount = 7300, State = "Cross River", LGA = "Calabar", FillingStation = "Oando", Date = new DateOnly(2024, 2, 24) },
                new FuelExpenseDto { Amount = 6900, State = "Plateau", LGA = "Jos", FillingStation = "Total", Date = new DateOnly(2024, 2, 23) },
                new FuelExpenseDto { Amount = 8200, State = "Bauchi", LGA = "Bauchi", FillingStation = "Conoil", Date = new DateOnly(2024, 2, 22) },
                new FuelExpenseDto { Amount = 8700, State = "Ondo", LGA = "Akure", FillingStation = "Mobil", Date = new DateOnly(2024, 2, 21) },
                new FuelExpenseDto { Amount = 9100, State = "Taraba", LGA = "Jalingo", FillingStation = "NNPC", Date = new DateOnly(2024, 2, 20) },
                new FuelExpenseDto { Amount = 5600, State = "Niger", LGA = "Minna", FillingStation = "Total", Date = new DateOnly(2024, 2, 19) }
            };

            // Store mock data in session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(mockExpenses));
            return mockExpenses;
        }

    }
}
