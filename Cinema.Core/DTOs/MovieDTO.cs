namespace Cinema.Core.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } =string.Empty;
        public string Description { get; set; } =string.Empty;
        public DateTime ReleaseDate { get; set; }
        public Double Rating { get; set; }
    }
}
