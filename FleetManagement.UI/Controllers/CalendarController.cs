using FleetManagement.UI.Models.DriverDTOs.DriverViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class CalendarController : BaseController
    {
        public IActionResult DriverSchedule()
        {
            // Generate mock events for demonstration. In a real scenario, you’d load shifts or off-duty events from a database.
            var events = new List<CalendarEvent>
            {
                new CalendarEvent { Title = "Morning Shift", Start = DateTime.Today.AddHours(8), End = DateTime.Today.AddHours(16), Color = "#28a745" },
                new CalendarEvent { Title = "Off Duty", Start = DateTime.Today.AddDays(2), Color = "#dc3545" },
                new CalendarEvent { Title = "Night Shift", Start = DateTime.Today.AddDays(4).AddHours(20), End = DateTime.Today.AddDays(5).AddHours(4), Color = "#ffc107" }
            };

            // Generate a mock list for other drivers' schedules.
            var otherDrivers = new List<DriverScheduleInfo>
            {
                new DriverScheduleInfo
                {
                    DriverName = "Jane Doe",
                    ShiftStart = DateTime.Today.AddHours(9),
                    ShiftEnd = DateTime.Today.AddHours(17),
                    IsOnDuty = true
                },
                new DriverScheduleInfo
                {
                    DriverName = "Mark Spencer",
                    ShiftStart = DateTime.Today.AddHours(10),
                    ShiftEnd = DateTime.Today.AddHours(18),
                    IsOnDuty = false
                },
                new DriverScheduleInfo
                {
                    DriverName = "Alice Johnson",
                    ShiftStart = DateTime.Today.AddHours(7),
                    ShiftEnd = DateTime.Today.AddHours(15),
                    IsOnDuty = true
                }
            };

            // Populate upcoming time off/holidays
            var upcoming = new List<TimeOffInfo>
            {
                new TimeOffInfo
                {
                    Title = "Holiday (4 days)",
                    StartDate = new DateTime(2025, 3, 1),
                    EndDate = new DateTime(2025, 3, 4),
                    Status = "Approved"
                },
                new TimeOffInfo
                {
                    Title = "Public Friday",
                    StartDate = new DateTime(2025, 4, 18),
                    EndDate = new DateTime(2025, 4, 18),
                    Status = "Holiday"
                },
                new TimeOffInfo
                {
                    Title = "Easter Monday",
                    StartDate = new DateTime(2025, 4, 21),
                    EndDate = new DateTime(2025, 4, 21),
                    Status = "Holiday"
                }
            };

            // Set allowed leave days (for the notice on the dashboard)
            int allowedLeaveDays = 30;  // for example, 30 days per year

            var model = new DriverScheduleViewModel
            {
                Events = events,
                OtherDrivers = otherDrivers,
                AllowedLeaveDays = allowedLeaveDays,
                UpcomingTimeOff = upcoming
            };

            return View("DriverSchedule", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitLeaveRequest(DateTime LeaveDate, int LeaveDays, string LeaveType, string Reason)
        {
            // Create a new LeaveRequest object
            var leaveRequest = new LeaveRequest
            {
                LeaveStartDate = LeaveDate,
                LeaveDays = LeaveDays,
                LeaveType = LeaveType,
                Reason = Reason,
                Status = "Pending",
                SubmittedOn = DateTime.Now
            };

            // Retrieve leave requests from session (if any)
            List<LeaveRequest> leaveRequests = new List<LeaveRequest>();
            var sessionData = HttpContext.Session.GetString("LeaveRequests");
            if (!string.IsNullOrEmpty(sessionData))
            {
                leaveRequests = JsonSerializer.Deserialize<List<LeaveRequest>>(sessionData);
            }

            // Add the new request and update session
            leaveRequests.Add(leaveRequest);
            HttpContext.Session.SetString("LeaveRequests", JsonSerializer.Serialize(leaveRequests));

            TempData["Message"] = "Leave request submitted and is pending approval.";
            // Redirect to the schedule/dashboard page, or a dedicated leave requests page
            return RedirectToAction("DriverSchedule", "Driver");
        }

    }
}
