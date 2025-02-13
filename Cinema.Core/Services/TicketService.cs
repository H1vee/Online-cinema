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
            return tickets.Select(t => new TicketDTO
            {
                Id = t.TicketID,
                MovieTitle = t.Showtime?.Movie?.Title ?? "Unknown",
                ShowDateTime = t.Showtime?.ShowDateTime ?? DateTime.MinValue,
                RowNumber = t.Seat?.RowNumber ?? 0,
                SeatNumber = t.Seat?.SeatNumber ?? 0,
                FinalPrice = t.FinalPrice,
                Status = t.Status
            }).ToList();
        }


        public async Task<TicketDTO?> GetTicketByIdAsync(int id)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null) return null;

            return new TicketDTO
            {
                Id = ticket.TicketID,
                MovieTitle = ticket.Showtime?.Movie?.Title ?? "Unknown",
                ShowDateTime = ticket.Showtime?.ShowDateTime ?? DateTime.MinValue,
                RowNumber = ticket.Seat?.RowNumber ?? 0,
                SeatNumber = ticket.Seat?.SeatNumber ?? 0,
                FinalPrice = ticket.FinalPrice,
                Status = ticket.Status
            };
        }

        public async Task AddTicketAsync(CreateTicketDTO ticketDto)
        {

            var showtime = await _unitOfWork.Showtimes.GetByIdAsync(ticketDto.ShowtimeID);
            if (showtime == null) throw new Exception($"Showtime with ID {ticketDto.ShowtimeID} does not exist.");

            var seat = await _unitOfWork.Seats.GetByIdAsync(ticketDto.SeatID);
            if (seat == null) throw new Exception($"Seat with ID {ticketDto.SeatID} does not exist.");

            var user = await _unitOfWork.Users.GetByIdAsync(ticketDto.UserID);
            if (user == null) throw new Exception($"User with ID {ticketDto.UserID} does not exist.");

            var rule = await _unitOfWork.PricingRules.GetByIdAsync(ticketDto.RuleID);
            if (rule == null) throw new Exception($"Pricing Rule with ID {ticketDto.RuleID} does not exist.");

            var ticket = new Ticket
            {
                ShowtimeID = ticketDto.ShowtimeID,
                SeatID = ticketDto.SeatID,
                UserID = ticketDto.UserID,
                SaleID = ticketDto.SaleID,
                RuleID = ticketDto.RuleID,
                FinalPrice = ticketDto.FinalPrice,
                Status = ticketDto.Status
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
