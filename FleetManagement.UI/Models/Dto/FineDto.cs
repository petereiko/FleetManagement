using FleetManagement.UI.Models.DriverDTOs;

namespace FleetManagement.UI.Models.Dto
{
    public class FineDto
    {
        public string Id { get; set; }
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public string Type { get; set; } // e.g. Toll, Speeding, Parking
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } // Paid, Unpaid, Disputed
        public string Description { get; set; }
    }

    public static class DriverExtensions
    {
        public static string FullName(this Driver d) => $"{d.FirstName} {d.LastName}";
    }


}
