namespace Cinema.Core.DTOs
{
    public class CreateTicketDTO
    {
        public int ShowtimeID { get; set; }  
        public int SeatID { get; set; }  
        public int UserID { get; set; }  
        public int? SaleID { get; set; }  
        public int RuleID { get; set; }  
        public decimal FinalPrice { get; set; }  
        public string Status { get; set; } = "Booked";  
    }
}