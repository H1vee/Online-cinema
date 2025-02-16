using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IGenreRepository : IRepository<Genre, int>
    {
        Task<IEnumerable<Genre>> GetAllWithMoviesAsync();
    }
}