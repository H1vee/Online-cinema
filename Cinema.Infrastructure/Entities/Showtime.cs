using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinema.Infrastructure.Entities
{
 public class Showtime
 {
  [Key]
  public int ShowtimeID { get; set; }

  public int MovieID { get; set; }

  public int HallID { get; set; }

  public DateTime ShowDateTime { get; set; }

  public TimeSpan StartTime { get; set; }

  [ForeignKey(nameof(MovieID))]
  public Movie Movie { get; set; }

  [ForeignKey(nameof(HallID))]
  public Hall Hall { get; set; }
 }   
}

