namespace Cinema.Infrastructure.Entities
{
    public class UserRoles{
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<UserRolesAssignments>? MovieActors { get; set; }
    }
}