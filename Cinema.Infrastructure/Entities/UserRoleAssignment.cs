using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Infrastructure.Entities
{
    public class UserRoleAssignment
    {
        public int UserID { get; set; }
    
        public int RoleID { get; set; }
    
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }
    
        [ForeignKey(nameof(RoleID))]
        public UserRole UserRole { get; set; }
    }
}

