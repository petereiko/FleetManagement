using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.Dto;
using FleetManagement.UI.Models.DummyModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class VehicleController : BaseController
    {
        private const string SessionKey = "FuelExpenses";
        public IActionResult Index()
        {
            var vehicles = GetVehiclesFromSession();
            return View(vehicles);
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
                new FuelExpenseDto {
                    Vehicle = "ABC123XY (Toyota Hilux)", Driver = "Adebayo Ogunlesi",
                    FuelType = "Diesel", Liters = 45, PricePerLiter = 650,
                    Amount = 29250, Odometer = 12500,
                    State = "Lagos", LGA = "Ikeja", FillingStation = "Total",
                    ReceiptNumber = "REC-001", Date = new DateOnly(2024, 3, 10)
                },
                new FuelExpenseDto {
                    Vehicle = "DEF456UV (Ford Ranger)", Driver = "Chinyere Obi",
                    FuelType = "Petrol", Liters = 50, PricePerLiter = 620,
                    Amount = 31000, Odometer = 14000,
                    State = "Abuja", LGA = "Garki", FillingStation = "Mobil",
                    ReceiptNumber = "REC-002", Date = new DateOnly(2024, 3, 9)
                },
                new FuelExpenseDto {
                    Vehicle = "GHI789WX (Nissan Navara)", Driver = "Ibrahim Musa",
                    FuelType = "Diesel", Liters = 40, PricePerLiter = 630,
                    Amount = 25200, Odometer = 9800,
                    State = "Kano", LGA = "Nassarawa", FillingStation = "NNPC",
                    ReceiptNumber = "REC-003", Date = new DateOnly(2024, 3, 8)
                },
                new FuelExpenseDto {
                    Vehicle = "JKL012YZ (Toyota Land Cruiser)", Driver = "Amaka Nwosu",
                    FuelType = "Petrol", Liters = 55, PricePerLiter = 615,
                    Amount = 33825, Odometer = 17800,
                    State = "Lagos", LGA = "Lekki", FillingStation = "Oando",
                    ReceiptNumber = "REC-004", Date = new DateOnly(2024, 3, 7)
                },
                new FuelExpenseDto {
                    Vehicle = "MNO345AB (Ford Everest)", Driver = "Peter Okon",
                    FuelType = "Diesel", Liters = 60, PricePerLiter = 640,
                    Amount = 38400, Odometer = 21000,
                    State = "Rivers", LGA = "Port Harcourt", FillingStation = "Total",
                    ReceiptNumber = "REC-005", Date = new DateOnly(2024, 3, 6)
                },
                new FuelExpenseDto {
                    Vehicle = "PQR678CD (Toyota Prado)", Driver = "Samuel Johnson",
                    FuelType = "Petrol", Liters = 48, PricePerLiter = 600,
                    Amount = 28800, Odometer = 15500,
                    State = "Ogun", LGA = "Abeokuta", FillingStation = "Conoil",
                    ReceiptNumber = "REC-006", Date = new DateOnly(2024, 3, 5)
                },
                new FuelExpenseDto {
                    Vehicle = "STU901EF (Mitsubishi Pajero)", Driver = "Maryam Bello",
                    FuelType = "Diesel", Liters = 53, PricePerLiter = 645,
                    Amount = 34185, Odometer = 17200,
                    State = "Kaduna", LGA = "Kaduna North", FillingStation = "NNPC",
                    ReceiptNumber = "REC-007", Date = new DateOnly(2024, 3, 4)
                },
                new FuelExpenseDto {
                    Vehicle = "VWX234GH (Toyota Tacoma)", Driver = "Yusuf Adamu",
                    FuelType = "Petrol", Liters = 49, PricePerLiter = 610,
                    Amount = 29890, Odometer = 14600,
                    State = "Kogi", LGA = "Lokoja", FillingStation = "Mobil",
                    ReceiptNumber = "REC-008", Date = new DateOnly(2024, 3, 3)
                },
                new FuelExpenseDto {
                    Vehicle = "YZA567IJ (Ford F-150)", Driver = "Ngozi Eze",
                    FuelType = "Diesel", Liters = 46, PricePerLiter = 625,
                    Amount = 28750, Odometer = 16800,
                    State = "Enugu", LGA = "Enugu East", FillingStation = "Total",
                    ReceiptNumber = "REC-009", Date = new DateOnly(2024, 3, 2)
                },
                new FuelExpenseDto {
                    Vehicle = "BCD890KL (Nissan Titan)", Driver = "Emmanuel Chukwu",
                    FuelType = "Petrol", Liters = 52, PricePerLiter = 635,
                    Amount = 33020, Odometer = 15900,
                    State = "Benue", LGA = "Makurdi", FillingStation = "Oando",
                    ReceiptNumber = "REC-010", Date = new DateOnly(2024, 3, 1)
                },
            };

            // Store mock data in session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(mockExpenses));
            return mockExpenses;
        }

        [HttpPost]
        [HttpPost]
        public IActionResult EditVehicle(Vehicle updatedVehicle)
        {
            var vehicles = HttpContext.Session.GetObjectFromJson<List<Vehicle>>("Vehicles")
                ?? new List<Vehicle>();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == updatedVehicle.Id);

            if (vehicle != null)
            {
                vehicle.Make = updatedVehicle.Make;
                vehicle.Model = updatedVehicle.Model;
                vehicle.Year = updatedVehicle.Year;
                vehicle.Status = updatedVehicle.Status;
                // Add more fields here if the modal starts including them later.

                HttpContext.Session.SetObjectAsJson("Vehicles", vehicles);
            }

            return RedirectToAction("Index");
        }


    }
}
