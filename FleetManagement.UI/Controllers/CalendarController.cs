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

            var model = new DriverScheduleViewModel
            {
                Events = events
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
