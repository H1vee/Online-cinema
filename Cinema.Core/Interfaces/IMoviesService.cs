using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<MovieDTO>> GetAllMoviesAsync();
        Task<MovieDTO?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(MovieDTO movieDto);
        Task<bool> DeleteMovieAsync(int id);
        Task<IEnumerable<MovieDTO>> GetMoviesWithRatingAbove(float rating);
    }
}
