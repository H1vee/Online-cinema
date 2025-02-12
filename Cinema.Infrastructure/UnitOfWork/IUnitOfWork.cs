using System;
using System.Threading.Tasks;
using Cinema.Infrastructure.Repositories.Interfaces;
using Cinema.Infrastructure.Data;

namespace Cinema.Infrastructure.UnitOfWork
{
   public interface IUnitOfWork  :IDisposable
   {
      IUserRepository Users { get; }
      IMovieRepository Movies { get; }
      ITicketRepository Tickets { get; }
      ISaleRepository Sales { get; }
      IShowtimeRepository Showtimes { get; }
      ApplicationDbContext Context { get; }
      Update-Showtime
      IHallRepository Halls { get; }
      Task<int> CompleteAsync(); // SaveChanges()

   } 
}

