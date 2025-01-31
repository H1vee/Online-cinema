using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
   public class TicketRepository:Repository<Ticket,int>, ITicketRepository
   {
       public TicketRepository(ApplicationDbContext context) : base(context){}

       public async Task<IEnumerable<Ticket>> GetTicketsBySaleId(int saleId)
       {
           return await _context.Tickets.Where(t=>t.SaleID == saleId).ToListAsync();
       }
   } 
}

