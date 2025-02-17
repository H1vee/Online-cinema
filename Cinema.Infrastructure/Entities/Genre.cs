using System.ComponentModel.DataAnnotations;


namespace Cinema.Infrastructure.Entities
{
   public class Genre
   {
      [Key]
      public int GenreID { get; set; }

      [Required]
      [MaxLength(100)]
      public string Name { get; set; }

      public ICollection<MovieGenre> MovieGenres { get; set; }
   } 
}

