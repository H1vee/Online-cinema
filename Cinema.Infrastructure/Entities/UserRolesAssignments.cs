namespace Cinema.Infrastructure.Entities
{
    public class UserRolesAssignments{
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RoleId { get; set; }
        public UserRoles UserRoles { get; set; } = null!;
    }
}