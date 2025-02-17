using System.ComponentModel.DataAnnotations;


namespace Cinema.Infrastructure.Entities
{
   public class UserRole
   {
       
       [Key]
       public int RoleID { get; set; }
   
       [Required]
       [MaxLength(50)]
       public string RoleName { get; set; }
   
       public ICollection<UserRoleAssignment> UserRoleAssignments { get; set; }
   } 
}

