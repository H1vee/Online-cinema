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
    public ISeatRepository Seats { get; }
    public IActorRepository Actors { get; } 
    public IGenreRepository Genres { get; }
    public IPricingRuleRepository PricingRules { get; }
    
    public ApplicationDbContext Context => _context;
    public UnitOfWork(ApplicationDbContext context, IUserRepository users, IMovieRepository movies,
      ITicketRepository tickets, ISaleRepository sales, IShowtimeRepository showtimes, IHallRepository halls, ISeatRepository seats, IPricingRuleRepository pricingRules,
      IActorRepository actors, IGenreRepository genres)
    {
      _context = context;
      Users = users;
      Movies = movies;
      Tickets = tickets;
      Sales = sales;
      Showtimes = showtimes;
      Halls = halls;
      Seats = seats;
      PricingRules = pricingRules;
      Actors = actors;
      Genres = genres;
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

