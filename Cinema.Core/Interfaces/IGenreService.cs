using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(int id);
        Task AddGenreAsync(CreateGenreDTO genreDto);
        Task<bool> DeleteGenreAsync(int id);
    }
}