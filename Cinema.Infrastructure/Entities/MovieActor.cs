
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinema.Infrastructure.Entities
{
   public class MovieActor
   {
      public int MovieID { get; set; }

      public int ActorID { get; set; }

      public int RoleID { get; set; }

      [ForeignKey(nameof(MovieID))]
      public Movie Movie { get; set; }

      [ForeignKey(nameof(ActorID))]
      public Actor Actor { get; set; }

      [ForeignKey(nameof(RoleID))]
      public Role Role { get; set; }
   } 
}

