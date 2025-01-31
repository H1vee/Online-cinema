using System;
using System.Threading.Tasks;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Infrastructure.UnitOfWork
{
   public interface IUnitOfWork  :IDisposable
   {
      IUserRepository Users { get; }
      IMovieRepository Movies { get; }
      ITicketRepository Tickets { get; }
      ISaleRepository Sales { get; }
      
      Task<int> CompleteAsync(); // SaveChanges()
   } 
}

