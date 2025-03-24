using FleetManagement.UI.Models.CompanyAssetDto;

namespace FleetManagement.UI.Models.DriverDTOs
{
    public class Driver
    {

        public string Id { get; set; } = "DR-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

        public string FirstName { get; set; } 
        public string MiddleName { get; set; }
        public string LastName { get; set; } 
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        // Employment details
        public string EmployeeId { get; set; } 
        public DateTime DateHired { get; set; }
        public string EmploymentStatus { get; set; } = "Active"; // Active, Inactive, Suspended

        // License information
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string LicenseCategory { get; set; } 

        // Vehicle Assignment
        public int? AssignedVehicleId { get; set; }
        public Vehicle? AssignedVehicle { get; set; } 

        // Documents
        public List<DriverDocument> Documents { get; set; } = new List<DriverDocument>();

        // Emergency Contact
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; } 
        public string Relationship { get; set; }
        
    }


    public class DriverDocument
    {
        public int Id { get; set; }
        public int DriverId { get; set; } 
        public string DocumentName { get; set; }
        public string FilePath { get; set; } 
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public Driver? Driver { get; set; } 
    }


}
