namespace Cinema.Infrastructure.Entities
{
    public class Genres{
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<MovieGenres>? MovieGenres { get; set; }
    }
}