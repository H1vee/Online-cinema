namespace Cinema.Core.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } =string.Empty;
        public string Description { get; set; } =string.Empty;
        public DateTime ReleaseDate { get; set; }
        public float? Rating { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> Genres { get; set; } = new();
        public List<string> Actors { get; set; } = new();
    }
}
