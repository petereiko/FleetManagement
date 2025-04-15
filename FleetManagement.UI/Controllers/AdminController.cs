using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.Dto;
using FleetManagement.UI.Models.DummyModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using FleetManagement.UI.Models;          // For Driver, Vehicle, etc.
    using FleetManagement.UI.Models.DriverDTOs;      // For AdminDashboardViewModel, MaintenanceTicket, etc.

    namespace YourNamespace.Controllers
    {
        public class AdminController : BaseController
        {
            public IActionResult Index()
            {
                // Retrieve tickets from session
                var sessionData = HttpContext.Session.GetString("MaintenanceTickets");
                var tickets = string.IsNullOrEmpty(sessionData)
                    ? new List<MaintenanceTicket>()
                    : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

                int pendingCount = tickets.Where(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold).Count();
                int acceptedCount = tickets.Where(t => t.IsApproved).Count();
                int rejectedCount = tickets.Where(t => t.IsRejected).Count();

                DateTime weekStart = DateTime.Now.AddDays(-7);
                var ticketsThisWeek = tickets.Where(t => t.DateLogged >= weekStart).ToList();
                int totalTicketsWeek = ticketsThisWeek.Count;
                int pendingTicketsWeek = ticketsThisWeek.Count(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold);

                // Retrieve drivers from session
                var drivers = GetDriversFromSession();

                //Retrieve vehicles from session
                var vehicles = GetVehiclesFromSession();
                int totalVehicles = vehicles.Count;

                // Load notifications from session
                var notificationsSession = HttpContext.Session.GetString("AdminNotifications");
                List<string> notifications = string.IsNullOrEmpty(notificationsSession)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(notificationsSession);

                var model = new AdminDashboardViewModel
                {
                    TotalAssets = totalVehicles,
                    TotalStaff = 75,
                    TotalReports = 15,
                    PendingTickets = pendingCount,
                    TotalAcceptedTickets = acceptedCount,
                    TotalRejectedTickets = rejectedCount,
                    TotalTicketsReceivedWeek = totalTicketsWeek,
                    PendingTicketsWeek = pendingTicketsWeek,
                    Notifications = notifications,
                    TotalDrivers = drivers.Count,
                    RecentAssets = new List<Asset>
                {
                    new Asset { Id = 1, Name = "Company Car", Condition = "Good" },
                    new Asset { Id = 2, Name = "Delivery Van", Condition = "Excellent" },
                    new Asset { Id = 3, Name = "Delivery Bike", Condition = "Fair" }
                },
                    RecentStaff = new List<Staff>
                {
                    new Staff { Id = 1, FullName = "Gbenga Johnson", PhoneNumber = "07011169508" },
                    new Staff { Id = 2, FullName = "Tobi Daniels", PhoneNumber = "09130507728" },
                    new Staff { Id = 3, FullName = "Abubakar Django", PhoneNumber = "08025412365" }
                },
                    RecentReports = new List<Report>
                {
                    new Report { Id = 1, Title = "Monthly Expense Report", DateFiled = DateTime.Now.AddDays(-2) },
                    new Report { Id = 2, Title = "Asset Maintenance Report", DateFiled = DateTime.Now.AddDays(-5) }
                },
                    ListOfDrivers = drivers
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
                var drivers = GetDriversFromSession();
                var vehicles = GetVehiclesFromSession();

                var model = new DriverVehicleMappingViewModel
                {
                    Drivers = drivers,
                    Vehicles = vehicles,
                    SelectedDriverId = drivers.FirstOrDefault()?.Id
                };

                return View(model);
            }

            [HttpPost]
            [IgnoreAntiforgeryToken]
            public IActionResult AssignVehicles(string selectedDriverId, List<string> vehicleIds)
            {
                if (string.IsNullOrEmpty(selectedDriverId))
                {
                    return Json(new { success = false, message = "No driver selected." });
                }

                var vehicles = GetVehiclesFromSession();

                // Unassign vehicles currently assigned to this driver but not in the new list.
                foreach (var v in vehicles.Where(v => v.AssignedDriverId == selectedDriverId).ToList())
                {
                    if (vehicleIds == null || !vehicleIds.Contains(v.Id))
                    {
                        v.IsAssigned = false;
                        v.AssignedDriverId = null;
                        v.AssignedDriverName = null;
                    }
                }

                // Assign selected vehicles to the driver
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

            // NEW: GET: /Admin/ListDrivers
            public IActionResult ListDrivers()
            {
                var drivers = GetDriversFromSession();
                return View(drivers);
            }

            // NEW: GET: /Admin/EditDriver?id=...
            public IActionResult EditDriver(string id)
            {
                var drivers = GetDriversFromSession();
                var driver = drivers.FirstOrDefault(d => d.Id == id);
                if (driver == null)
                {
                    return NotFound();
                }
                return View(driver);
            }

            // NEW: POST: /Admin/EditDriver
            [HttpPost]
            [IgnoreAntiforgeryToken]
            public IActionResult EditDriver(Driver model)
            {
                var drivers = GetDriversFromSession();
                var driver = drivers.FirstOrDefault(d => d.Id == model.Id);
                if (driver == null)
                {
                    return NotFound();
                }

                // Update properties
                driver.FirstName = model.FirstName;
                driver.LastName = model.LastName;
                driver.Email = model.Email;
                driver.PhoneNumber = model.PhoneNumber;
                driver.Address = model.Address;
                driver.DateOfBirth = model.DateOfBirth;
                driver.Gender = model.Gender;
                driver.LicenseNumber = model.LicenseNumber;
                driver.LicenseExpiryDate = model.LicenseExpiryDate;
                driver.LicenseCategory = model.LicenseCategory;
                driver.EmploymentStatus = model.EmploymentStatus;
                driver.EmergencyContactName = model.EmergencyContactName;
                driver.EmergencyContactPhone = model.EmergencyContactPhone;
                driver.Relationship = model.Relationship;

                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return RedirectToAction("ListDrivers");
            }

            // NEW: POST: /Admin/DeactivateDriver?id=...
            [HttpPost]
            [IgnoreAntiforgeryToken]
            public IActionResult DeactivateDriver(string id)
            {
                var drivers = GetDriversFromSession();
                var driver = drivers.FirstOrDefault(d => d.Id == id);
                if (driver == null)
                {
                    return NotFound();
                }

                driver.EmploymentStatus = "Inactive";
                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return RedirectToAction("ListDrivers");
            }


            private string GetDriverNameById(string driverId)
            {
                var drivers = GetDriversFromSession();
                return drivers.FirstOrDefault(d => d.Id == driverId)?.FullName;
            }

            [HttpGet]
            public IActionResult VehicleDetails(string vehicleId)
            {
                var vehicles = GetVehiclesFromSession();
                var vehicle = vehicles.FirstOrDefault(v => v.Id == vehicleId);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return PartialView("_VehicleDetails", vehicle);
            }

            [HttpGet]
            public IActionResult DrillDownDetails(string category)
            {
                var sessionData = HttpContext.Session.GetString("MaintenanceTickets");
                var tickets = string.IsNullOrEmpty(sessionData)
                    ? new List<MaintenanceTicket>()
                    : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

                List<MaintenanceTicket> filteredTickets = new List<MaintenanceTicket>();

                switch (category.ToLower())
                {
                    case "total received":
                        filteredTickets = tickets;
                        break;
                    case "pending":
                        filteredTickets = tickets.Where(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold).ToList();
                        break;
                    case "accepted":
                        filteredTickets = tickets.Where(t => t.IsApproved).ToList();
                        break;
                    case "rejected":
                        filteredTickets = tickets.Where(t => t.IsRejected).ToList();
                        break;
                    default:
                        break;
                }

                return PartialView("_DrillDownDetails", filteredTickets);
            }

            public IActionResult RegisterDriver()
            {
                return View();
            }

            [HttpPost]
            [IgnoreAntiforgeryToken]
            public IActionResult RegisterDriver(Driver model)
            {
                var drivers = GetDriversFromSession();
                drivers.Add(model);
                HttpContext.Session.SetString("Drivers", JsonSerializer.Serialize(drivers));
                return RedirectToAction("ListDrivers");
            }


            public IActionResult VehicleTracker()
            {
                // Retrieve vehicles from session
                var sessionData = HttpContext.Session.GetString("Vehicles");
                var vehicles = string.IsNullOrEmpty(sessionData)
                    ? new List<Vehicle>()
                    : JsonSerializer.Deserialize<List<Vehicle>>(sessionData);

                // For testing, if no vehicles have geo data, assign sample coordinates (e.g., Lagos)
                foreach (var v in vehicles)
                {
                    if (v.Latitude == 0 || v.Longitude == 0)
                    {
                        // Example: Random coordinates around Lagos
                        v.Latitude = 6.5244 + (new Random().NextDouble() - 0.5) * 0.1;
                        v.Longitude = 3.3792 + (new Random().NextDouble() - 0.5) * 0.1;
                    }
                }
                return View("VehicleTracker", vehicles);
            }

        }
    }

}
