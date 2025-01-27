using System.ComponentModel.DataAnnotations;

namespace Cinema.Infrastructure.Entities
{
    public class PricingRule
    {
        [Key]
        public int RuleID { get; set; }

        [Required]
        [MaxLength(50)]
        public string SeatType { get; set; }

        [Required]
        [MaxLength(50)]
        public string TimeOfDay { get; set; }

        [Required]
        [MaxLength(50)]
        public string DayType { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}

