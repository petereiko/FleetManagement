namespace FleetManagement.UI.Models.DummyModels
{
    public class AdminDashboardViewModel
    {
        public int TotalAssets { get; set; }
        public int TotalStaff { get; set; }
        public int TotalReports { get; set; }
        public int PendingTickets { get; set; }
        public int TotalDrivers { get; set; }



        public int TotalAcceptedTickets { get; set; }
        public int TotalRejectedTickets { get; set; }
        public int TotalTicketsReceivedWeek { get; set; }
        public int PendingTicketsWeek { get; set; }
        public List<int> DailyTicketCounts { get; set; }  // e.g., ticket count for each of the last 7 days
        public List<string> Last7Days { get; set; }         // e.g., ["Mon", "Tue", ...]
        public List<string> Notifications { get; set; }     // List of notification messages



        public IEnumerable<Asset> RecentAssets { get; set; }
        public IEnumerable<Staff> RecentStaff { get; set; }
        public IEnumerable<Report> RecentReports { get; set; }
    }
}
