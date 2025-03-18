using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text.Json;

namespace FleetManagement.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminTicketController : Controller
    {
        private const string SessionKey = "MaintenanceTickets";

        public IActionResult Index(string status = "", string search = "", int page = 1, int pageSize = 10)
        {
            var tickets = GetTicketsFromSession();

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status))
            {
                switch (status.ToLower())
                {
                    case "approved":
                        tickets = tickets.Where(t => t.IsApproved).ToList();
                        break;
                    case "rejected":
                        tickets = tickets.Where(t => t.IsRejected).ToList();
                        break;
                    case "pending":
                        tickets = tickets.Where(t => t.IsSubmitted && !t.IsApproved && !t.IsRejected && !t.IsOnHold).ToList();
                        break;
                    case "onhold":
                        tickets = tickets.Where(t => t.IsOnHold).ToList();
                        break;
                }
            }

            // Filter by search term on DriverName or CarLicense
            if (!string.IsNullOrEmpty(search))
            {
                tickets = tickets.Where(t =>
                    (!string.IsNullOrEmpty(t.DriverName) && t.DriverName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(t.CarLicense) && t.CarLicense.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Order by DateLogged descending and paginate
            tickets = tickets.OrderByDescending(t => t.DateLogged).ToList();
            int totalTickets = tickets.Count;
            int totalPages = (int)Math.Ceiling(totalTickets / (double)pageSize);
            var pagedTickets = tickets.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalTickets = totalTickets;
            ViewBag.FilterStatus = status;
            ViewBag.Search = search;

            return View(pagedTickets);
        }

        // New AJAX action to update ticket status dynamically
        [HttpPost]
        public IActionResult UpdateStatusAjax([FromBody] UpdateStatusRequest request)
        {
            var tickets = GetTicketsFromSession();
            var ticket = tickets.FirstOrDefault(t => t.TicketNumber == request.ticketNumber);
            if (ticket != null)
            {
                // Reset all statuses
                ticket.IsApproved = false;
                ticket.IsRejected = false;
                ticket.IsOnHold = false;

                switch (request.status.ToLower())
                {
                    case "approved":
                        ticket.IsApproved = true;
                        break;
                    case "rejected":
                        ticket.IsRejected = true;
                        break;
                    case "onhold":
                        ticket.IsOnHold = true;
                        break;
                }

                SaveTicketsToSession(tickets);
                return Json(new { success = true, newStatus = request.status });
            }
            return Json(new { success = false });
        }

        private List<MaintenanceTicket> GetTicketsFromSession()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            var tickets = string.IsNullOrEmpty(sessionData)
                ? new List<MaintenanceTicket>()
                : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);
            return tickets;
        }

        private void SaveTicketsToSession(List<MaintenanceTicket> tickets)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(tickets));
        }

        [HttpGet]
        public IActionResult TicketDetails(string ticketNumber)
        {
            var tickets = GetTicketsFromSession();
            var ticket = tickets.FirstOrDefault(t => t.TicketNumber == ticketNumber);
            if (ticket == null)
            {
                return NotFound();
            }
            return PartialView("_TicketDetails", ticket);
        }

    }

    public class UpdateStatusRequest
    {
        public string ticketNumber { get; set; }
        public string status { get; set; }
    }
}

