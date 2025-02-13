using System;

namespace Cinema.Core.DTOs
{
  public class CreateTicketDTO
  {
        public int Movieid { get; set; }
        public DateTime ShowDateTime { get; set; }
        public int RowNumber { get; set; } 
        public int SeatNumber { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
  }  
}
