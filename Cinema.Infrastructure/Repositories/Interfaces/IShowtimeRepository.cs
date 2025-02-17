using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IShowtimeRepository:IRepository<Showtime,int>
    {
        Task<IEnumerable<Showtime>> GetShowtimesByDate(DateTime date);
        Task<IEnumerable<Showtime>> GetShowtimesByGenre(string genreName);
        Task<IEnumerable<Showtime>> GetShowtimesByDurationAsync(int maxDuration);
    }
}

