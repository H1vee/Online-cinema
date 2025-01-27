using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinema.Infrastructure.Entities
{
    public class Seat
    {
        [Key]
        public int SeatID { get; set; }

        public int HallID { get; set; }

        [Range(1, int.MaxValue)]
        public int RowNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int SeatNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string SeatType { get; set; }

        [ForeignKey(nameof(HallID))]
        public Hall Hall { get; set; }
    }
}

