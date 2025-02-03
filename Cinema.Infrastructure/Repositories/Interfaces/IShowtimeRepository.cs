using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IShowtimeRepository:IRepository<Showtime,int>
    {
        Task<IEnumerable<Showtime>>GetUpcomingShowtimes();
    }
}

