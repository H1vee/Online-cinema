using System.Threading.Tasks;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Infrastructure.UnitOfWork
{
  public class UnitOfWork:IUnitOfWork
  {
    private readonly ApplicationDbContext _context;
    public IUserRepository Users { get; }
    public IMovieRepository Movies { get; }
    public ITicketRepository Tickets { get; }
    public ISaleRepository Sales { get; }
    public IShowtimeRepository Showtimes { get; }

    public IHallRepository Halls { get; }
    
    public ApplicationDbContext Context => _context;
    public UnitOfWork(ApplicationDbContext context, IUserRepository users, IMovieRepository movies,
      ITicketRepository tickets, ISaleRepository sales, IShowtimeRepository showtimes, IHallRepository halls)
    {
      _context = context;
      Users = users;
      Movies = movies;
      Tickets = tickets;
      Sales = sales;
      Showtimes = showtimes;
      Halls = halls;
    }
    
    public async Task<int> CompleteAsync()
    {
      return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
      _context.Dispose();
    }
  }  
}

