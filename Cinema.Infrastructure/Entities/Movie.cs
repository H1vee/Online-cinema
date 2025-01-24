namespace Cinema.Infrastructure.Entities
{
    public class Movie{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal? Rating { get; set;}

        public ICollection<MovieActors>? MovieActors { get; set; }
        public ICollection<MovieGenres>? MovieGenres { get; set; }
        public ICollection<ShowTimes>? ShowTimes { get; set; }
    }
}