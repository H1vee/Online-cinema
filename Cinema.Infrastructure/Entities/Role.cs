using System.ComponentModel.DataAnnotations;

namespace Cinema.Infrastructure.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        [MaxLength(100)]
        public string RoleName { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
    }


