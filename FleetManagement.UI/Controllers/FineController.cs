using FleetManagement.UI.Models.DriverDTOs;
using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FleetManagement.UI.Controllers
{
    public class FineController : BaseController
    {
        private const string SessionKey = "Fines";


        private List<FineDto> GetFinesFromSession()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (string.IsNullOrEmpty(json)) return new List<FineDto>();
            return JsonConvert.DeserializeObject<List<FineDto>>(json);
        }

        private void SaveFinesToSession(List<FineDto> fines)
        {
            var json = JsonConvert.SerializeObject(fines);
            HttpContext.Session.SetString(SessionKey, json);
        }

        public IActionResult Index(string status = "All", string driverId = "All", string search = "")
        {
            var fines = GetFinesFromSession();
            var drivers = GetDrivers();

            if (status != "All")
                fines = fines.Where(f => f.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

            if (driverId != "All")
                fines = fines.Where(f => f.DriverId == driverId).ToList();

            if (!string.IsNullOrWhiteSpace(search))
                fines = fines.Where(f =>
                    f.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) == true ||
                    f.Type?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) == true ||
                    drivers.FirstOrDefault(d => d.Id == f.DriverId)?.FullName()?.Contains(search, StringComparison.OrdinalIgnoreCase) == true
                ).ToList();

            ViewBag.Drivers = drivers;
            ViewBag.Statuses = new List<string> { "All", "Paid", "Unpaid", "Disputed" };
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentDriver = driverId;
            ViewBag.SearchTerm = search;

            return View(fines);
        }


        [HttpPost]
        public IActionResult AddFine(FineDto fine)
        {
            fine.Id = Guid.NewGuid().ToString();
            var driver = GetDrivers().FirstOrDefault(d => d.Id == fine.DriverId);
            fine.DriverName = driver != null ? $"{driver.FirstName} {driver.LastName}" : "Unknown";

            var fines = GetFinesFromSession();
            fines.Add(fine);
            SaveFinesToSession(fines);

            TempData["SuccessMessage"] = "Fine added successfully.";
            return RedirectToAction("Index");
        }

        private List<Driver> GetDrivers()
        {
            // Use your GetDriversFromSession method
            return HttpContext.Session.GetString("Drivers") is string json
                ? JsonConvert.DeserializeObject<List<Driver>>(json)
                : new List<Driver>();
        }
    }

}
