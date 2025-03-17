namespace FleetManagement.UI.Models.CompanyAssetDto
{
    public class Vehicle
    {
        public string Id { get; set; } 
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; } // Vehicle Identification Number
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string EngineNumber { get; set; }
        public string ChassisNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public decimal Mileage { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; } // Manual/Automatic
        public string InsuranceCompany { get; set; }
        public DateTime InsuranceExpiryDate { get; set; }
        public DateTime RoadWorthyExpiryDate { get; set; }
        public string Status { get; set; } // Active, Inactive, Under Maintenance

        public List<VehicleDocument> Documents { get; set; } = new List<VehicleDocument>();
    }

    public class VehicleDocument
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string DocumentName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
