using System.ComponentModel.DataAnnotations;

namespace Cinema.Infrastructure.Entities
{
  public class Hall
  {
    [Key]
    public int HallID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }

    public ICollection<Seat> Seats { get; set; }
    public ICollection<Showtime> Showtimes { get; set; }
  }  
}

