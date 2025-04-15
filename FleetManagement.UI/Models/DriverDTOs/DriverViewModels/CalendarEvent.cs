namespace FleetManagement.UI.Models.DriverDTOs.DriverViewModels
{
    public class DriverScheduleViewModel
    {
        // The events for the calendar – shifts, off-duty dates etc.
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();
        public List<DriverScheduleInfo> OtherDrivers { get; set; } = new List<DriverScheduleInfo>();
        public int AllowedLeaveDays { get; set; } = 30; // e.g., maximum leave days allowed in the year
        public List<TimeOffInfo> UpcomingTimeOff { get; set; } = new List<TimeOffInfo>();

    }

    public class DriverScheduleInfo
    {
        public string DriverName { get; set; } = "";
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public bool IsOnDuty { get; set; }
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

    public class TimeOffInfo
    {
        public string Title { get; set; } = "";   // e.g. "Easter Monday"
        public DateTime StartDate { get; set; }   // e.g. new DateTime(2025, 04, 21)
        public DateTime EndDate { get; set; }     // e.g. new DateTime(2025, 04, 21)
        public string Status { get; set; } = "";  // e.g. "Approved", "Holiday", "Pending"
    }
}
