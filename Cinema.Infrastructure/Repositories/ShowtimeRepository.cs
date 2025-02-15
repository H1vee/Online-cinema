using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class ShowtimeRepository:Repository<Showtime,int>, IShowtimeRepository
    {
        public ShowtimeRepository(ApplicationDbContext context) : base(context){ }

        public async Task<IEnumerable<Showtime>> GetAllAsync()
        {
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<Showtime?> GetByIdAsync(int id)
        {
            return await _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.ShowtimeID == id);
        }

        public async Task<IEnumerable<Showtime>>GetUpcomingShowtimes()
        {
            return await _context.Showtimes.Where(s=> s.ShowDateTime > DateTime.UtcNow).ToListAsync();
        }
    }
}

