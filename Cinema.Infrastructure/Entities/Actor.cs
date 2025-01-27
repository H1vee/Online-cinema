using System.ComponentModel.DataAnnotations;


namespace Cinema.Infrastructure.Entities
{
    public class Actor
    {
        [Key]
        public int ActorID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        [MaxLength(100)]
        public string Nationality { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}

