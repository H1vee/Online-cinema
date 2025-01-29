using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Infrastructure.Entities {
 public class MovieGenre
 {
     public int MovieID { get; set; }

     public int GenreID { get; set; }

     [ForeignKey(nameof(MovieID))]
     public Movie Movie { get; set; }

     [ForeignKey(nameof(GenreID))]
     public Genre Genre { get; set; }
 }
 }

