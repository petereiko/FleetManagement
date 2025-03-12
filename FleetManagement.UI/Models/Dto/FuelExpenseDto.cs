namespace FleetManagement.UI.Models.Dto
{
    public class FuelExpenseDto
    {
        public decimal Amount { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string FillingStation { get; set; }
        public IFormFile Receipt { get; set; }
        public DateOnly Date { get; set; }

    }
}
