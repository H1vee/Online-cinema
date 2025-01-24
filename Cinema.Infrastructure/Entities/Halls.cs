namespace Cinema.Infrastructure.Entities
{
    public class Halls{
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; } = null!;
        public ICollection<ShowTimes>? ShowTimes { get; set; }
        public ICollection<Seat>? Seat { get; set; }
    }
}