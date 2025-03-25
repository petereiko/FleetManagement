using FleetManagement.UI.Models.CompanyAssetDto;

namespace FleetManagement.UI.Models.DriverDTOs.DriverViewModels
{
    public class DriverProfileViewModel
    {
        public Driver Driver { get; set; } = new Driver();
        public Vehicle? AssignedVehicle { get; set; }
        public List<Trip> UpcomingTrips { get; set; } = new List<Trip>();
        public List<Trip> CompletedTrips { get; set; } = new List<Trip>();
        public EarningsSummary Earnings { get; set; } = new EarningsSummary();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public PerformanceMetrics Performance { get; set; } = new PerformanceMetrics();
    }

    // Example supporting classes:
    public class Trip
    {
        public int TripId { get; set; }
        public string PickupLocation { get; set; } = "";
        public string DropoffLocation { get; set; } = "";
        public DateTime TripDate { get; set; }
        public decimal Fare { get; set; }
        public string Status { get; set; } = "";
    }

    public class EarningsSummary
    {
        public decimal WeeklyEarnings { get; set; }
        public decimal MonthlyEarnings { get; set; }
    }

    public class Notification
    {
        public string Message { get; set; } = "";
        public DateTime Date { get; set; }
    }

    public class PerformanceMetrics
    {
        public double Rating { get; set; }
        public int CompletedTrips { get; set; }
    }

}
