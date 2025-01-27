using System.ComponentModel.DataAnnotations;


namespace Cinema.Infrastructure.Entities
{
public class User
{
    
    [Key]
    public int UserID { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    public ICollection<UserRoleAssignment> UserRoleAssignments { get; set; }
}    
}

