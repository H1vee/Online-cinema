using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Cinema.Infrastructure.Repositories
{
    public class MovieRepository:Repository<Movie,int>,IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context){ }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }
        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.MovieID == id);
        }
        public async Task<IEnumerable<Movie>> GetMoviesWithRatingAbove(float rating)
        {
            return await _context.Movies.Where(m => m.Rating > rating).ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetRecentMoviesAsync()
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);
            return await _context.Movies
                .Where(m => m.ReleaseDate <= DateTime.UtcNow && m.ReleaseDate >= oneYearAgo)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetUpcomingMoviesAsync()
        {
            return await _context.Movies
                .Where(m => m.ReleaseDate > DateTime.UtcNow)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }
    }
}