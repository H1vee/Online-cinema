namespace Cinema.Infrastructure.Entities
{
    public class User{
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? RegistrationDate { get; set; } = null!;
        public ICollection<UserRolesAssignments>? UserRolesAssignments { get; set; }
    }
}