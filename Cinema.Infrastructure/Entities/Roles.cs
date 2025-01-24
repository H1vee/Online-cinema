namespace Cinema.Infrastructure.Entities
{
    public class Roles{
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieActors>? MovieActors { get; set; }
    }
}