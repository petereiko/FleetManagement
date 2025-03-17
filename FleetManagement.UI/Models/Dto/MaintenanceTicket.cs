namespace FleetManagement.UI.Models.Dto
{
    public class MaintenanceTicket
    {
        public string TicketNumber { get; set; } = $"TKT-{new Random().Next(10000, 99999)}";
        public List<MaintenanceItem> Items { get; set; } = new List<MaintenanceItem>();
        public bool IsApproved { get; set; } = false;
        public bool IsSubmitted { get; set; } = false;
        public bool IsRejected { get; set; } = false; 

        public decimal TotalCost => Items.Sum(i => i.TotalPrice);
        public int TotalQuantity => Items.Sum(i => i.Quantity);
        public DateTime DateLogged { get; set; } = DateTime.Now;


    }

    public class MaintenanceItem
    {
        public string Part { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
