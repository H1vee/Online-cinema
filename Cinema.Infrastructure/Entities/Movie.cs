using System.ComponentModel.DataAnnotations;

namespace Cinema.Infrastructure.Entities
{ 
 public class Movie
 {
  [Key]
  public int MovieID { get; set; }

  [Required]
  [MaxLength(200)]
  public string Title { get; set; }

  public string Description { get; set; }

  public DateTime? ReleaseDate { get; set; }

  [Range(0, 10)]
  public float? Rating { get; set; }

  [Required]
  public TimeSpan Duration { get; set; }

  [Required]
  [MaxLength(255)]
  public string TrailerURL { get; set; }

  public string PosterURL { get; set; }

  public ICollection<MovieGenre> MovieGenres { get; set; }
  public ICollection<MovieActor> MovieActors { get; set; }
 }   
}

