using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class ActorRepository : Repository<Actor, int>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Actor>> GetAllWithMoviesAsync()
        {
            return await _context.Actors
                .Include(a => a.MovieActors)
                .ThenInclude(ma => ma.Movie)
                .ToListAsync();
        }
    }
}