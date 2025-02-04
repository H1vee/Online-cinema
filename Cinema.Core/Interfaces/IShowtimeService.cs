using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync();
        Task<ShowtimeDTO?> GetShowtimeByIdAsync(int id);
        Task AddShowtimeAsync(ShowtimeDTO showtimeDto);
        Task<bool> DeleteShowtimeAsync(int id);
        Task<IEnumerable<ShowtimeDTO>> GetUpcomingShowtimesAsync();
    }
}
