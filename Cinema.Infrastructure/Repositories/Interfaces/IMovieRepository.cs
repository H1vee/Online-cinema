using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface IMovieRepository:IRepository<Movie,int>
    {
        Task<IEnumerable<Movie>> GetMoviesWithRatingAbove(float rating);
        Task<IEnumerable<Movie>> GetRecentMoviesAsync();
        Task<IEnumerable<Movie>> GetUpcomingMoviesAsync();
    
    }
}