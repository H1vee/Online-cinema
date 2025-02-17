using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDTO>> GetAllShowtimesAsync();
        Task<ShowtimeDTO?> GetShowtimeByIdAsync(int id);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDateAsync(DateTime date);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByGenreAsync(string genreName);
        Task<IEnumerable<ShowtimeDTO>> GetShowtimesByDurationAsync(int maxDuration);
        Task AddShowtimeAsync(CreateShowtimeDTO showtimeDto);
        Task<bool> DeleteShowtimeAsync(int id);
    }
}