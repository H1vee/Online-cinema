using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface ITicketRepository:IRepository<Ticket,int>
    {
        Task<IEnumerable<Ticket>> GetTicketsBySaleId(int saleId);
    }
}

