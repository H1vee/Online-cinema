using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class SaleRepository:Repository<Sale,int>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext context) : base(context){}

        public async Task<IEnumerable<Sale>> GetSalesByUserId(int userId)
        {
            return await _context.Sales.Where(s => s.UserID == userId).ToListAsync();
        }
    }
}

