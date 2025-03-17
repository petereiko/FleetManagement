using FleetManagement.UI.Models.CompanyAssetDto;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.UI.Controllers
{
    public class AssetController : Controller
    {
        private static List<Vehicle> Vehicles = new List<Vehicle>();

        public IActionResult VehicleList()
        {
            return View(Vehicles);
        }

        public IActionResult RegisterVehicle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterVehicle(Vehicle vehicle, List<IFormFile> documents)
        {
            if (ModelState.IsValid)
            {
                vehicle.Id = Guid.NewGuid().ToString();
                vehicle.RegistrationDate = DateTime.Now;
                vehicle.Documents = new List<VehicleDocument>();

                if (documents != null && documents.Any())
                {
                    foreach (var document in documents)
                    {
                        var fileName = Path.GetFileName(document.FileName);
                        var filePath = Path.Combine("wwwroot/uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            document.CopyTo(stream);
                        }

                        vehicle.Documents.Add(new VehicleDocument
                        {
                            DocumentName = fileName,
                            FilePath = "/uploads/" + fileName,
                            UploadDate = DateTime.Now
                        });
                    }
                }

                Vehicles.Add(vehicle);
                return RedirectToAction("VehicleList");
            }
            return View(vehicle);
        }
    }
}
