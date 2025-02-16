using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface IMovieService
{
    Task<IEnumerable<MovieDTO>> GetAllMoviesAsync();
    Task<MovieDTO?> GetMovieByIdAsync(int id);
    Task AddMovieAsync(CreateMovieDTO movieDto);
    Task<bool> DeleteMovieAsync(int id);
    Task<IEnumerable<MovieDTO>> GetMoviesWithRatingAbove(float rating);
    Task<IEnumerable<MovieDTO>> GetRecentMoviesAsync();  
    Task<IEnumerable<MovieDTO>> GetUpcomingMoviesAsync(); 
}
}
