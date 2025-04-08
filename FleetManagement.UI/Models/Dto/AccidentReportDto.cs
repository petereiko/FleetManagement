using System.ComponentModel.DataAnnotations;

namespace FleetManagement.UI.Models.Dto
{
    public class AccidentReportDto
    {
        public string ReportNumber { get; set; } = $"RPT-{DateTime.Now:yyyyMMddHHmmss}";

        [Required]
        public DateTime AccidentDate { get; set; }

        [Required]
        public string DriverName { get; set; }

        [Required]
        public string VehicleLicense { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string LGA { get; set; }

        [Required]
        public DamageLevel LevelOfDamage { get; set; }

        public string Description { get; set; }
        public string Landmark { get; set; }

        // Allow multiple photos/scans of the accident
        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }

    // Models/DamageLevel.cs
    public enum DamageLevel
    {
        Mild,
        Medium,
        Critical,
        Fatal
    }
}
