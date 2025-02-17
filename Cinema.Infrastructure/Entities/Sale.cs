using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cinema.Infrastructure.Entities
{
  public class Sale
  {
    [Key]
    public int SaleID { get; set; }

    public int UserID { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TotalAmount { get; set; }

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UserID))]
    public User User { get; set; }

    public ICollection<Ticket> Tickets { get; set; }  
  }  
}

