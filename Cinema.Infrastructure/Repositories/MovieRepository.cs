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

        public async Task<IEnumerable<Movie>> GetMoviesWithRatingAbove(float rating)
        {
            return await _context.Movies.Where(m => m.Rating > rating).ToListAsync();
        }
    }
}

