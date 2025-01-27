using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinema.Infrastructure.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        public int ShowtimeID { get; set; }

        public int SeatID { get; set; }

        public int UserID { get; set; }

        public int? SaleID { get; set; }

        public int RuleID { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal FinalPrice { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [ForeignKey(nameof(ShowtimeID))]
        public Showtime Showtime { get; set; }

        [ForeignKey(nameof(SeatID))]
        public Seat Seat { get; set; }

        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        [ForeignKey(nameof(SaleID))]
        public Sale Sale { get; set; }

        [ForeignKey(nameof(RuleID))]
        public PricingRule PricingRule { get; set; }
    }
}

