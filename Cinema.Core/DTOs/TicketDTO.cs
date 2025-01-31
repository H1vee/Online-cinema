namespace Cinema.Core.DTOs
{
  public class TicketDTO
  {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime ShowDateTime { get; set; }
        public int RowNumber { get; set; } 
        public int SeatNumber { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
  }  
}

