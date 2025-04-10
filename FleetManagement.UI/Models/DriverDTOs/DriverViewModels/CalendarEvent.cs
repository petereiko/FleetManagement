namespace FleetManagement.UI.Models.DriverDTOs.DriverViewModels
{
    public class DriverScheduleViewModel
    {
        // The events for the calendar – shifts, off-duty dates etc.
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
    }

    public class CalendarEvent
    {
        public string Title { get; set; } = "";
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Color { get; set; } = ""; // Optional, for visual differentiation
    }

    public class LeaveRequest
    {
        public DateTime LeaveStartDate { get; set; }
        public int LeaveDays { get; set; }
        public string LeaveType { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // e.g. "Pending", "Approved", etc.
        public DateTime SubmittedOn { get; set; }
    }

}
