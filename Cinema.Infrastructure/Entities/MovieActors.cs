namespace Cinema.Infrastructure.Entities
{
    public class MovieActors{
        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int RoleId { get; set; }
        public Roles Role { get; set; } = null!;
    }
}