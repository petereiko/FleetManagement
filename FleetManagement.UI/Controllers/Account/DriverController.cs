using FleetManagement.UI.Models.CompanyAssetDto;
using FleetManagement.UI.Models.DriverDTOs.DriverViewModels;
using FleetManagement.UI.Models.DriverDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FleetManagement.UI.Controllers.Account
{
    public class DriverController : BaseController
    {
        // GET: /Driver/Profile
        // GET: /Driver/Profile
        public IActionResult Profile()
        {
            // For demonstration, use the shared methods from BaseController.
            var drivers = GetDriversFromSession();
            // Assume the logged in driver is the first one (or use session/claims)
            var driver = drivers[0];
            var vehicle = GetVehiclesFromSession().Find(v => v.AssignedDriverId.ToString() == driver.Id.ToString());

            // For demonstration, create mock lists for trips, earnings, notifications, etc.
            var model = new DriverProfileViewModel
            {
                Driver = driver,
                AssignedVehicle = vehicle,
                UpcomingTrips = new List<Trip>
                {
                    new Trip { TripId = 101, PickupLocation = "Location A", DropoffLocation = "Location B", TripDate = DateTime.Now.AddDays(1), Fare = 1200, Status = "Scheduled" },
                    new Trip { TripId = 102, PickupLocation = "Location C", DropoffLocation = "Location D", TripDate = DateTime.Now.AddDays(2), Fare = 1500, Status = "Scheduled" }
                },
                CompletedTrips = new List<Trip>
                {
                    new Trip { TripId = 90, PickupLocation = "Location X", DropoffLocation = "Location Y", TripDate = DateTime.Now.AddDays(-3), Fare = 1000, Status = "Completed" },
                    new Trip { TripId = 91, PickupLocation = "Location Z", DropoffLocation = "Location W", TripDate = DateTime.Now.AddDays(-5), Fare = 1800, Status = "Completed" }
                },
                Earnings = new EarningsSummary { WeeklyEarnings = 5000, MonthlyEarnings = 20000 },
                Notifications = new List<Notification>
                {
                    new Notification { Message = "New trip assigned", Date = DateTime.Now.AddMinutes(-30) },
                    new Notification { Message = "Vehicle maintenance scheduled", Date = DateTime.Now.AddHours(-1) }
                },
                Performance = new PerformanceMetrics { Rating = 4.7, CompletedTrips = 150 }
            };

            return View(model);
        }
    
      
    }
}