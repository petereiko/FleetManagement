using FleetManagement.UI.Models.CompanyAssetDto;

namespace FleetManagement.UI.Models.ServiceHubs
{
    public class MasterVehicleList
    {
        public static class VehicleStore
        {
            // This will hold the “master” list loaded from session
            public static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        }
    }
}
