using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.Dto;
using FleetManagement.UI.Models.DummyModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            // Retrieve tickets from session
            var sessionData = HttpContext.Session.GetString("MaintenanceTickets");
            var tickets = string.IsNullOrEmpty(sessionData)
                ? new List<MaintenanceTicket>()
                : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

            // Count tickets that are submitted but not approved, rejected, or on hold (i.e. Pending)
            int pendingCount = tickets
                .Where(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold)
                .Count();

            var drivers = GetDriversFromSession();

            // Count accepted and rejected tickets
            int acceptedCount = tickets.Where(t => t.IsApproved).Count();
            int rejectedCount = tickets.Where(t => t.IsRejected).Count();

            // Compute weekly statistics (last 7 days)
            DateTime weekStart = DateTime.Now.AddDays(-7);
            var ticketsThisWeek = tickets.Where(t => t.DateLogged >= weekStart).ToList();
            int totalTicketsWeek = ticketsThisWeek.Count;
            int pendingTicketsWeek = ticketsThisWeek.Count(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold);


            // Load notifications from session
            var notificationsSession = HttpContext.Session.GetString("AdminNotifications");
            List<string> notifications = string.IsNullOrEmpty(notificationsSession)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(notificationsSession);

            // Create the dashboard model (use real data in your application)
            var model = new AdminDashboardViewModel
            {
                TotalAssets = 120,
                TotalStaff = 75,
                TotalReports = 15,
                PendingTickets = pendingCount,
                TotalAcceptedTickets = acceptedCount,
                TotalRejectedTickets = rejectedCount,
                TotalTicketsReceivedWeek = totalTicketsWeek,
                PendingTicketsWeek = pendingTicketsWeek,
                Notifications = notifications,
                TotalDrivers=drivers.Count,
                RecentAssets = new List<Asset>
        {
            new Asset { Id = 1, Name = "Company Car", Condition = "Good" },
            new Asset { Id = 2, Name = "Delivery Van", Condition = "Excellent" },
            new Asset { Id = 3, Name = "Delivery Bike", Condition = "Fair" }
        },
                RecentStaff = new List<Staff>
        {
            new Staff { Id = 1, FullName = "Gbenga Johnson", Position = "Manager" },
            new Staff { Id = 2, FullName = "Tobi Daniels", Position = "Technician" },
            new Staff { Id = 3, FullName = "Abubakar Django", Position = "Driver" }
        },
                RecentReports = new List<Report>
        {
            new Report { Id = 1, Title = "Monthly Expense Report", DateFiled = DateTime.Now.AddDays(-2) },
            new Report { Id = 2, Title = "Asset Maintenance Report", DateFiled = DateTime.Now.AddDays(-5) }
        }
            };

            return View(model);
        }


        public IActionResult ExportTicketsCsv()
        {
            var sessionData = HttpContext.Session.GetString("MaintenanceTickets");
            var tickets = string.IsNullOrEmpty(sessionData)
                ? new List<MaintenanceTicket>()
                : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

            var csv = new StringBuilder();
            csv.AppendLine("TicketNumber,DriverName,CarLicense,DateLogged,Status");
            foreach (var t in tickets)
            {
                var status = t.IsApproved ? "Accepted" : t.IsRejected ? "Rejected" : t.IsOnHold ? "On Hold" : t.IsSubmitted ? "Pending" : "Draft";
                csv.AppendLine($"{t.TicketNumber},{t.DriverName},{t.CarLicense},{t.DateLogged:yyyy-MM-dd HH:mm:ss},{status}");
            }
            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "Tickets.csv");
        }

        [HttpGet]
        public IActionResult GetNotifications()
        {
            var notificationsSession = HttpContext.Session.GetString("AdminNotifications");
            List<string> notifications = string.IsNullOrEmpty(notificationsSession)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(notificationsSession);
            return Json(notifications);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public IActionResult ClearNotifications()
        {
            HttpContext.Session.Remove("AdminNotifications");
            return Json(new { success = true });
        }



        // GET: /Admin/ManageDriverVehicles
        public IActionResult ManageDriverVehicles()
        {
            // Load drivers and vehicles from session (or generate mock data if absent)
            var drivers = GetDriversFromSession();
            var vehicles = GetVehiclesFromSession();

            // Set a default selected driver (for example, the first driver)
            var model = new DriverVehicleMappingViewModel
            {
                Drivers = drivers,
                Vehicles = vehicles,
                SelectedDriverId = drivers.FirstOrDefault()?.Id
            };

            return View(model);
        }

        // POST: /Admin/AssignVehicles
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult AssignVehicles(string selectedDriverId, List<string> vehicleIds)
        {
            if (string.IsNullOrEmpty(selectedDriverId))
            {
                return Json(new { success = false, message = "No driver selected." });
            }

            var vehicles = GetVehiclesFromSession();

            // Unassign any vehicle that is currently assigned to the selected driver but is not in the new list.
            foreach (var v in vehicles.Where(v => v.AssignedDriverId == selectedDriverId).ToList())
            {
                if (vehicleIds == null || !vehicleIds.Contains(v.Id))
                {
                    v.IsAssigned = false;
                    v.AssignedDriverId = null;
                    v.AssignedDriverName = null;
                }
            }

            // For each vehicle in vehicleIds, if it's unassigned or already assigned to this driver, assign it.
            foreach (var vehicleId in vehicleIds ?? new List<string>())
            {
                var vehicle = vehicles.FirstOrDefault(v => v.Id == vehicleId);
                if (vehicle != null)
                {
                    if (!vehicle.IsAssigned || vehicle.AssignedDriverId == selectedDriverId)
                    {
                        vehicle.IsAssigned = true;
                        vehicle.AssignedDriverId = selectedDriverId;
                        vehicle.AssignedDriverName = GetDriverNameById(selectedDriverId);
                    }
                }
            }

            SaveVehiclesToSession(vehicles);
            return Json(new { success = true });
        }

        // Helper: Retrieve drivers from session or generate mock data
        private List<Driver> GetDriversFromSession()
        {
            var json = HttpContext.Session.GetString("Drivers");
            if (string.IsNullOrEmpty(json))
            {
                var drivers = new List<Driver>
            {
                new Driver { Id = "D1", FullName = "Eddie Hoyte" },
                new Driver { Id = "D2", FullName = "Jane Smith" },
                new Driver { Id = "D3", FullName = "Gbenga Tokunbo" },
                new Driver { Id = "D4", FullName = "Chukwudi Michael Smith" },
                new Driver { Id = "D5", FullName = "Musa Shehu" },
                new Driver { Id = "D6", FullName = "Ayomide Johnson" },
                new Driver { Id = "D7", FullName = "Bob Brown" }
            };
                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return drivers;
            }
            return JsonSerializer.Deserialize<List<Driver>>(json);
        }

        // Helper: Retrieve vehicles from session or generate mock data
        private List<Vehicle> GetVehiclesFromSession()
        {
            var json = HttpContext.Session.GetString("Vehicles");
            if (string.IsNullOrEmpty(json))
            {
                var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = "V1", Make = "Toyota", Model = "Camry", Year = 2020, VIN = "VIN001", LicensePlate = "ABC123", Color = "White", EngineNumber = "ENG001", ChassisNumber = "CHS001", RegistrationDate = DateTime.Now.AddYears(-1), LastServiceDate = DateTime.Now.AddMonths(-3), Mileage = 15000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(6), RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = true, AssignedDriverId = "D1", AssignedDriverName = "Eddie Hoyte" },
                new Vehicle { Id = "V2", Make = "Honda", Model = "Civic", Year = 2019, VIN = "VIN002", LicensePlate = "XYZ789", Color = "Black", EngineNumber = "ENG002", ChassisNumber = "CHS002", RegistrationDate = DateTime.Now.AddYears(-2), LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 20000, FuelType = "Petrol", Transmission = "Manual", InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(8), RoadWorthyExpiryDate = DateTime.Now.AddMonths(5), Status = "Active", IsAssigned = true, AssignedDriverId = "D2", AssignedDriverName = "Jane Smith" },
                new Vehicle { Id = "V3", Make = "Ford", Model = "Focus", Year = 2021, VIN = "VIN003", LicensePlate = "FOC456", Color = "Blue", EngineNumber = "ENG003", ChassisNumber = "CHS003", RegistrationDate = DateTime.Now.AddMonths(-10), LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 10000, FuelType = "Diesel", Transmission = "Automatic", InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(10), RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = true, AssignedDriverId = "D3", AssignedDriverName = "Gbenga Tokunbo" },
                new Vehicle { Id = "V4", Make = "Chevrolet", Model = "Malibu", Year = 2018, VIN = "VIN004", LicensePlate = "MAL321", Color = "Silver", EngineNumber = "ENG004", ChassisNumber = "CHS004", RegistrationDate = DateTime.Now.AddYears(-3), LastServiceDate = DateTime.Now.AddMonths(-4), Mileage = 30000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(5), RoadWorthyExpiryDate = DateTime.Now.AddMonths(3), Status = "Active", IsAssigned = false },
                new Vehicle { Id = "V5", Make = "Nissan", Model = "Altima", Year = 2022, VIN = "VIN005", LicensePlate = "ALT654", Color = "Red", EngineNumber = "ENG005", ChassisNumber = "CHS005", RegistrationDate = DateTime.Now.AddMonths(-2), LastServiceDate = DateTime.Now.AddMonths(-1), Mileage = 5000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "InsureCo", InsuranceExpiryDate = DateTime.Now.AddMonths(12), RoadWorthyExpiryDate = DateTime.Now.AddMonths(7), Status = "Active", IsAssigned = false },
                new Vehicle { Id = "V6", Make = "Mercedes Benz", Model = "E360", Year = 2022, VIN = "VIN006", LicensePlate = "GBV856", Color = "Gray", EngineNumber = "ENG006", ChassisNumber = "CHS006", RegistrationDate = DateTime.Now.AddMonths(-3), LastServiceDate = DateTime.Now.AddMonths(-2), Mileage = 8000, FuelType = "Diesel", Transmission = "Automatic", InsuranceCompany = "PremiumInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(9), RoadWorthyExpiryDate = DateTime.Now.AddMonths(8), Status = "Active", IsAssigned = false },
                new Vehicle { Id = "V7", Make = "Toyota", Model = "Tundra", Year = 2018, VIN = "VIN007", LicensePlate = "VALL423", Color = "Blue", EngineNumber = "ENG007", ChassisNumber = "CHS007", RegistrationDate = DateTime.Now.AddYears(-4), LastServiceDate = DateTime.Now.AddMonths(-6), Mileage = 40000, FuelType = "Diesel", Transmission = "Manual", InsuranceCompany = "SafeInsure", InsuranceExpiryDate = DateTime.Now.AddMonths(7), RoadWorthyExpiryDate = DateTime.Now.AddMonths(4), Status = "Active", IsAssigned = false },
                new Vehicle { Id = "V8", Make = "Honda", Model = "Crosstour", Year = 2022, VIN = "VIN008", LicensePlate = "JUH664", Color = "White", EngineNumber = "ENG008", ChassisNumber = "CHS008", RegistrationDate = DateTime.Now.AddMonths(-1), LastServiceDate = DateTime.Now.AddDays(-20), Mileage = 3000, FuelType = "Petrol", Transmission = "Automatic", InsuranceCompany = "AutoSafe", InsuranceExpiryDate = DateTime.Now.AddMonths(11), RoadWorthyExpiryDate = DateTime.Now.AddMonths(6), Status = "Active", IsAssigned = false }
            };

                HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
                return vehicles;
            }
            return JsonSerializer.Deserialize<List<Vehicle>>(json);
        }

        private void SaveVehiclesToSession(List<Vehicle> vehicles)
        {
            HttpContext.Session.SetString("Vehicles", JsonSerializer.Serialize(vehicles));
        }

        private string GetDriverNameById(string driverId)
        {
            var drivers = GetDriversFromSession();
            return drivers.FirstOrDefault(d => d.Id == driverId)?.FullName;
        }

        [HttpGet]
        public IActionResult VehicleDetails(string vehicleId)
        {
            // Retrieve the list of vehicles from session (or your mock data)
            var vehicles = GetVehiclesFromSession();
            var vehicle = vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle == null)
            {
                return NotFound();
            }
            return PartialView("_VehicleDetails", vehicle);
        }


    }
}
