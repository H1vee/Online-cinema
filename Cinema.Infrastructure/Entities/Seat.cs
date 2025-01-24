namespace Cinema.Infrastructure.Entities
{
    public class Seat{
        public int Id { get; set;}
        public int SeatNumber { get; set;} 
        public int RowNumber { get; set;} 
        public string SeatType { get; set;} 
        public int HallId { get; set; }
        public Halls Halls { get; set; } = null!;
    }
}