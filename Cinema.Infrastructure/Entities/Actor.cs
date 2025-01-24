namespace Cinema.Infrastructure.Entities
{
    public class Actor{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public ICollection<MovieActors>? MovieActors { get; set; }
    }
}