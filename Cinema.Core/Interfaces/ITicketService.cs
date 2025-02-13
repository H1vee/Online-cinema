using Cinema.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTO>> GetAllTicketsAsync();
        Task<TicketDTO?> GetTicketByIdAsync(int id);
        Task AddTicketAsync(CreateTicketDTO ticketDto);
        Task<bool> DeleteTicketAsync(int id);
        Task<IEnumerable<TicketDTO>> GetTicketsBySaleIdAsync(int saleId);
    }
}
