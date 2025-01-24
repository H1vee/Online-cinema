namespace Cinema.Infrastructure.Entities
{
    public class ShowTimes{
        public int HallId { get; set; }
        public Halls Halls { get; set; } = null!;
        
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public DateTime ShowDate { get; set; } = null!;
        public DateTime ShowStart { get; set; } = null!;
    }
}