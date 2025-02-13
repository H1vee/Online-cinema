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
       public async Task<IEnumerable<Ticket>> GetAllAsync()
       {
           return await _context.Tickets
               .Include(t => t.Showtime)
               .ThenInclude(s => s.Movie)
               .Include(t => t.Seat)
               .ToListAsync();
       }

       public async Task<Ticket?> GetByIdAsync(int id)
       {
           return await _context.Tickets
               .Include(t => t.Showtime)
               .ThenInclude(s => s.Movie)
               .Include(t => t.Seat)
               .FirstOrDefaultAsync(t => t.TicketID == id);
       }

       public TicketRepository(ApplicationDbContext context) : base(context){}

       public async Task<IEnumerable<Ticket>> GetTicketsBySaleId(int saleId)
       {
           return await _context.Tickets.Where(t=>t.SaleID == saleId).ToListAsync();
       }
   } 
}

