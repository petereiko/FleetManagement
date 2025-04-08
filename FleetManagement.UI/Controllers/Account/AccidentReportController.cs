using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FleetManagement.UI.Controllers.Account
{
    public class AccidentReportController : Controller
    {
        private const string SessionKey = "AccidentReports";

        public IActionResult Index()
        {
            var reports = GetReportsFromSession();
            return View(reports);
        }

        public IActionResult ReportDetails(string id)
        {
            var report = GetReportsFromSession().FirstOrDefault(r => r.ReportNumber == id);
            if (report == null) return NotFound();
            return PartialView("_ReportDetails", report);
        }

        public IActionResult Create()
        {
            PopulateViewBags();
            return View(new AccidentReportDto { AccidentDate = DateTime.Today });
        }

        [HttpPost]
        public IActionResult Create(AccidentReportDto model)
        {
            if (!ModelState.IsValid)
            {
                PopulateViewBags();
                return View(model);
            }
            var reports = GetReportsFromSession();
            model.ReportNumber = $"RPT-{DateTime.Now:yyyyMMddHHmmss}";
            reports.Add(model);
            SaveReportsToSession(reports);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string id)
        {
            var report = GetReportsFromSession().FirstOrDefault(r => r.ReportNumber == id);
            if (report == null) return NotFound();
            PopulateViewBags();
            return View(report);
        }

        [HttpPost]
        public IActionResult Edit(AccidentReportDto model)
        {
            if (!ModelState.IsValid)
            {
                PopulateViewBags();
                return View(model);
            }
            var reports = GetReportsFromSession();
            var existing = reports.FirstOrDefault(r => r.ReportNumber == model.ReportNumber);
            if (existing == null) return NotFound();
            existing.AccidentDate = model.AccidentDate;
            existing.DriverName = model.DriverName;
            existing.VehicleLicense = model.VehicleLicense;
            existing.State = model.State;
            existing.LGA = model.LGA;
            existing.LevelOfDamage = model.LevelOfDamage;
            existing.Description = model.Description;
            existing.Landmark = model.Landmark;
            SaveReportsToSession(reports);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var reports = GetReportsFromSession();
            var report = reports.FirstOrDefault(r => r.ReportNumber == id);
            if (report != null)
            {
                reports.Remove(report);
                SaveReportsToSession(reports);
            }
            return RedirectToAction(nameof(Index));
        }

        private List<AccidentReportDto> GetReportsFromSession()
        {
            var data = HttpContext.Session.GetString(SessionKey);
            return string.IsNullOrEmpty(data)
                ? new List<AccidentReportDto>()
                : JsonConvert.DeserializeObject<List<AccidentReportDto>>(data);
        }

        private void SaveReportsToSession(List<AccidentReportDto> reports)
        {
            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(reports));
        }

        private void PopulateViewBags()
        {
            ViewBag.States = new List<string> 
            { 
                "Abia","Adamawa","Akwa Ibom","Anambra","Bauchi","Bayelsa","Benue",
                "Borno","Cross River","Delta","Ebonyi","Edo","Ekiti","Enugu",
                "Gombe","Imo","Jigawa","Kaduna","Kano","Katsina","Kebbi","Kogi",
                "Kwara","Lagos","Nasarawa","Niger","Ogun","Ondo","Osun","Oyo",
                "Plateau","Rivers","Sokoto","Taraba","Yobe","Zamfara","FCT" 
            };
            ViewBag.LGAs = new Dictionary<string, List<string>> 
            {
                ["Lagos"] = new List<string> { "Ikeja", "Epe", "Ikorodu", "Surulere" },
                ["FCT"] = new List<string> { "Gwagwalada", "Kuje", "Abaji", "Bwari" },
            };
            ViewBag.DamageLevels = Enum.GetValues(typeof(DamageLevel))
                .Cast<DamageLevel>()
                .Select(d => new SelectListItem { Value = d.ToString(), Text = d.ToString() })
                .ToList();
        }
    }
}
