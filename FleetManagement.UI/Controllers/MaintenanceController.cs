using FleetManagement.UI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FleetManagement.UI.Controllers
{
    public class MaintenanceController : Controller
    {
        private const string SessionKey = "MaintenanceTickets";

        public IActionResult TicketList()
        {
            var tickets = GetTicketsFromSession();
            ViewBag.TotalTickets = tickets.Count;
            return View(tickets);
        }

        // Updated AddTicket accepts optional parameters for driver details
        public IActionResult AddTicket(string driverName = "", string carLicense = "")
        {
            ViewBag.Parts = new List<string> { "Engine", "Brakes", "Tires", "Battery", "Oil Filter" };

            var tickets = GetTicketsFromSession();
            var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

            if (activeTicket == null)
            {
                activeTicket = new MaintenanceTicket
                {
                    DateLogged = DateTime.Now,
                    DriverName = driverName,
                    CarLicense = carLicense
                };
                tickets.Add(activeTicket);
                SaveTicketsToSession(tickets);
            }
            else
            {
                // If driver details are passed in, update the active ticket
                if (!string.IsNullOrEmpty(driverName))
                    activeTicket.DriverName = driverName;
                if (!string.IsNullOrEmpty(carLicense))
                    activeTicket.CarLicense = carLicense;

                SaveTicketsToSession(tickets);
            }

            return View(activeTicket);
        }

        [HttpPost]
        public IActionResult AddTicketItem(MaintenanceItem item)
        {
            var tickets = GetTicketsFromSession();
            var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

            if (activeTicket != null)
            {
                activeTicket.Items.Add(item);
                SaveTicketsToSession(tickets);

                return Json(new
                {
                    success = true,
                    part = item.Part,
                    quantity = item.Quantity,
                    unitPrice = item.UnitPrice.ToString("N2"),
                    totalPrice = (item.Quantity * item.UnitPrice).ToString("N2")
                });
            }

            return Json(new { success = false });
        }

        public IActionResult SendRequest()
        {
            var tickets = GetTicketsFromSession();
            var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

            if (activeTicket != null)
            {
                activeTicket.IsSubmitted = true;
                SaveTicketsToSession(tickets);
            }

            return RedirectToAction("TicketList");
        }

        // Retrieve tickets from the session
        private List<MaintenanceTicket> GetTicketsFromSession()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            var tickets = string.IsNullOrEmpty(sessionData)
                ? new List<MaintenanceTicket>()
                : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

            // Ensure that older tickets get a timestamp if missing.
            foreach (var ticket in tickets)
            {
                if (ticket.DateLogged == default)
                {
                    ticket.DateLogged = DateTime.Now;
                }
            }

            return tickets;
        }

        private void SaveTicketsToSession(List<MaintenanceTicket> tickets)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(tickets));
        }
    }




    //public class MaintenanceController : Controller
    //{
    //    private const string SessionKey = "MaintenanceTickets";

    //    public IActionResult TicketList()
    //    {
    //        var tickets = GetTicketsFromSession();
    //        ViewBag.TotalTickets = tickets.Count;
    //        return View(tickets);
    //    }

    //    public IActionResult AddTicket()
    //    {
    //        ViewBag.Parts = new List<string> { "Engine", "Brakes", "Tires", "Battery", "Oil Filter" };

    //        var tickets = GetTicketsFromSession();
    //        var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

    //        if (activeTicket == null)
    //        {
    //            activeTicket = new MaintenanceTicket
    //            {
    //                DateLogged = DateTime.Now // ✅ Set DateLogged when creating a new ticket
    //            };
    //            tickets.Add(activeTicket);
    //            SaveTicketsToSession(tickets);
    //        }

    //        return View(activeTicket);
    //    }


    //    [HttpPost]
    //    public IActionResult AddTicketItem(MaintenanceItem item)
    //    {
    //        var tickets = GetTicketsFromSession();
    //        var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

    //        if (activeTicket != null)
    //        {
    //            activeTicket.Items.Add(item);
    //            SaveTicketsToSession(tickets);

    //            return Json(new
    //            {
    //                success = true,
    //                part = item.Part,
    //                quantity = item.Quantity,
    //                unitPrice = item.UnitPrice.ToString("N2"),
    //                totalPrice = (item.Quantity * item.UnitPrice).ToString("N2")
    //            });
    //        }

    //        return Json(new { success = false });
    //    }


    //    public IActionResult SendRequest()
    //    {
    //        var tickets = GetTicketsFromSession();
    //        var activeTicket = tickets.FirstOrDefault(t => !t.IsSubmitted);

    //        if (activeTicket != null)
    //        {
    //            activeTicket.IsSubmitted = true;
    //            SaveTicketsToSession(tickets);
    //        }

    //        return RedirectToAction("TicketList");
    //    }


    //    private List<MaintenanceTicket> GetTicketsFromSession()
    //    {
    //        var sessionData = HttpContext.Session.GetString(SessionKey);
    //        var tickets = string.IsNullOrEmpty(sessionData)
    //            ? new List<MaintenanceTicket>()
    //            : JsonSerializer.Deserialize<List<MaintenanceTicket>>(sessionData);

    //        var random = new Random();
    //        foreach (var ticket in tickets)
    //        {
    //            if (ticket.DateLogged == default) // ✅ Ensure older tickets also get a timestamp
    //            {
    //                ticket.DateLogged = DateTime.Now;
    //            }

    //            if (ticket.IsSubmitted)
    //            {
    //                int statusChance = random.Next(0, 5); // 0 = Pending, 1 = Approved, 2 = Rejected
    //                if (statusChance == 1)
    //                {
    //                    ticket.IsApproved = true;
    //                }
    //                else if (statusChance == 2)
    //                {
    //                    ticket.IsRejected = true;
    //                }
    //            }
    //        }

    //        return tickets;
    //    }


    //    private void SaveTicketsToSession(List<MaintenanceTicket> tickets)
    //    {
    //        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(tickets));
    //    }





    //}


}
