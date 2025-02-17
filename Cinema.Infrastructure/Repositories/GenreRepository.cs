using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre, int>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Genre>> GetAllWithMoviesAsync()
        {
            return await _context.Genres
                .Include(g => g.MovieGenres)
                .ThenInclude(mg => mg.Movie)
                .ToListAsync();
        }
    }
}