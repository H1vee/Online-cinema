using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IActorRepository : IRepository<Actor, int>
    {
        Task<IEnumerable<Actor>> GetAllWithMoviesAsync();
    }
}