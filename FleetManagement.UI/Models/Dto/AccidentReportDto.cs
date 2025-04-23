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

        // 🆕 New fields from the document
        public string RoadCondition { get; set; }               // e.g., Wet, Dry, Potholes, etc.
        public string Visibility { get; set; }                  // e.g., Clear, Foggy, Rainy
        public string WeatherCondition { get; set; }            // e.g., Rain, Sun, Dust
        public string VehicleMovementDescription { get; set; }  // How it happened

        public string PoliceStationReportedTo { get; set; }
        public string OfficerInCharge { get; set; }

        public string DriverStatement { get; set; }
        public string WitnessName { get; set; }
        public string WitnessPhone { get; set; }

        public string InternalComment { get; set; } // Admin use only


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
