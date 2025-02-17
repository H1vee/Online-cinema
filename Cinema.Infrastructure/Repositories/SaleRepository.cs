using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class SaleRepository:Repository<Sale,int>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) : base(context){}

        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.User)
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.SaleID == id);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.User)
                .Include(s => s.Tickets)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByUserId(int userId)
        {
            return await _context.Sales
                .Include(s => s.User)
                .Include(s => s.Tickets)
                .Where(s => s.UserID == userId)
                .ToListAsync();
        }
    }
}

