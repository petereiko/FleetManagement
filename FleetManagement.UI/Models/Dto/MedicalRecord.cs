namespace FleetManagement.UI.Models.Dto
{
    // MedicalRecord.cs
    public class MedicalRecord
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime NextDueDate { get; set; }
        public string Clinic { get; set; }
        public string Notes { get; set; }
    }

    // EmergencyContact.cs
    public class EmergencyContact
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPrimary { get; set; }
    }

    // DriverCareRecord.cs
    public class DriverCareRecord
    {
        public string DriverId { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<EmergencyContact> EmergencyContacts { get; set; } = new();
    }

}
