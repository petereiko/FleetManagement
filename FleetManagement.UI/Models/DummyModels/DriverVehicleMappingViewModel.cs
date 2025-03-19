using FleetManagement.UI.Models.CompanyAssetDto;

namespace FleetManagement.UI.Models.DummyModels
{
    public class DriverVehicleMappingViewModel
    {
        public List<Driver> Drivers { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public string SelectedDriverId { get; set; }

    }
}
