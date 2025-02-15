using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Infrastructure.Repositories
{
    public class SeatRepository : Repository<Seat, int>, ISeatRepository
    {
        public SeatRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}