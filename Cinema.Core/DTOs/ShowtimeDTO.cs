namespace Cinema.Core.DTOs
{
    public class ShowtimeDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime ShowDateTime { get; set; }
    }
}
