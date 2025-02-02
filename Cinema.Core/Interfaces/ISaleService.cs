using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDTO>> GetAllSalesAsync();
        Task<SaleDTO?> GetSaleByIdAsync(int id);
        Task AddSaleAsync(SaleDTO saleDto);
        Task<bool> DeleteSaleAsync(int id);
        Task<IEnumerable<SaleDTO>> GetSalesByUserIdAsync(int userId);
    }
}
