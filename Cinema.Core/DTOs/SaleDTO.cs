using System;
using System.Collections.Generic;

namespace Cinema.Core.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SimpleTicketDTO> Tickets { get; set; } = new();
    }
}

