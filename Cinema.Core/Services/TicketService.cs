using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }

        public async Task<TicketDTO?> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            return ticket == null ? null : _mapper.Map<TicketDTO>(ticket);
        }

        public async Task AddTicketAsync(CrateTicketDTO ticketDto)
        {
            var movieExists = await _unitOfWork.Movies.GetByIdAsync(showtimeDto.MovieId);
            if (movieExists == null)
            {
                throw new Exception($"Movie with ID {ticketDto.MovieId} does not exist.");
            }
            var ticket = new Ticket
            {
                MovieID = ticketDto.MovieId,
                ShowDateTime = ticketDto.ShowDateTime,
                RowNumber = ticketDTO.RowNumber,
                SeatNumber = ticketDTO.SeatNumber,
                FinalPrice = ticketDTO.FinalPrice,
                Status = ticketDTO.Status
            };
            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null || ticket.Status == "Purchased") 
            {
                return false;
            }
            
            _unitOfWork.Tickets.Remove(ticket);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<TicketDTO>> GetTicketsBySaleIdAsync(int saleId)
        {
            var tickets = await _unitOfWork.Tickets.GetTicketsBySaleId(saleId);
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }
    }
}
