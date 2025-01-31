using Cinema.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Cinema.Infrastructure.Repositories.Interfaces
{
    public interface ISaleRepository:IRepository<Sale,int>
    {
        Task<IEnumerable<Sale>> GetSalesByUserId(int serId);
    }
}

