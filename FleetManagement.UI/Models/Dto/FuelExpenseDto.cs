namespace FleetManagement.UI.Models.Dto
{
    public class FuelExpenseDto
    {
        public DateOnly Date { get; set; }
        public string Vehicle { get; set; }        
        public string Driver { get; set; }          
        public string FuelType { get; set; }        
        public int Liters { get; set; }             
        public decimal PricePerLiter { get; set; }  
        public decimal Amount { get; set; }         
        public int Odometer { get; set; }           
        public string FillingStation { get; set; }  
        public string ReceiptNumber { get; set; }   
        public IFormFile Receipt { get; set; }   
        public string State { get; set; }
        public string LGA { get; set; }
    }

}
