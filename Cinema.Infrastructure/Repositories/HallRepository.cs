using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.Repositories.Interfaces;

namespace Cinema.Infrastructure.Repositories
{
  public class HallRepository:Repository<Hall,int>, IHallRepository
  {
      public HallRepository(ApplicationDbContext context) : base(context){}
  }  
    
}

