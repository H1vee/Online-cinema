namespace Cinema.Infrastructure.Entities
{
    public class MovieGenres{
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int GenreId { get; set; }
        public Genres Genres { get; set; } = null!;
    }
}