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
      IHallRepository Halls { get; }
      ISeatRepository Seats { get; }
      IActorRepository Actors { get; } 
      IGenreRepository Genres { get; }
      IPricingRuleRepository PricingRules { get; }
      Task<int> CompleteAsync(); // SaveChanges()
   } 
}

