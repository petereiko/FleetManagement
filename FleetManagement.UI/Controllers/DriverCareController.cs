using FleetManagement.UI.Models.DriverDTOs;
using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class DriverCareController : BaseController
    {
        private const string DriverKey = "Drivers";
        private const string CareRecordKey = "DriverCareRecords";

        private List<Driver> GetDrivers() =>
            JsonSerializer.Deserialize<List<Driver>>(HttpContext.Session.GetString(DriverKey) ?? "[]");

        private List<DriverCareRecord> GetCareRecords()
        {
            var json = HttpContext.Session.GetString(CareRecordKey);
            if (string.IsNullOrEmpty(json)) return new List<DriverCareRecord>();
            return JsonSerializer.Deserialize<List<DriverCareRecord>>(json);
        }

        private void SaveCareRecords(List<DriverCareRecord> records)
        {
            HttpContext.Session.SetString(CareRecordKey, JsonSerializer.Serialize(records));
        }

        public IActionResult Index()
        {
            var drivers = GetDrivers();
            return View(drivers);
        }

        public IActionResult Details(string id)
        {
            var driver = GetDrivers().FirstOrDefault(x => x.Id == id);
            if (driver == null) return NotFound();

            var records = GetCareRecords();
            var careRecord = records.FirstOrDefault(r => r.DriverId == id) ?? new DriverCareRecord { DriverId = id };

            return View(new Tuple<Driver, DriverCareRecord>(driver, careRecord));
        }

        [HttpPost]
        public IActionResult AddMedical(MedicalRecord record, string driverId)
        {
            var careRecords = GetCareRecords();
            var care = careRecords.FirstOrDefault(x => x.DriverId == driverId);
            if (care == null)
            {
                care = new DriverCareRecord { DriverId = driverId };
                careRecords.Add(care);
            }

            record.Date = DateTime.Today;
            care.MedicalRecords.Add(record);
            SaveCareRecords(careRecords);

            return RedirectToAction("Details", new { id = driverId });
        }

        [HttpPost]
        public IActionResult AddContact(EmergencyContact contact, string driverId)
        {
            var careRecords = GetCareRecords();
            var care = careRecords.FirstOrDefault(x => x.DriverId == driverId);
            if (care == null)
            {
                care = new DriverCareRecord { DriverId = driverId };
                careRecords.Add(care);
            }

            care.EmergencyContacts.Add(contact);
            SaveCareRecords(careRecords);

            return RedirectToAction("Details", new { id = driverId });
        }
    }

}
