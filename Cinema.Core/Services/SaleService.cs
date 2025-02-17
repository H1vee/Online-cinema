using AutoMapper;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure.Entities;
using Cinema.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SaleDTO>> GetAllSalesAsync()
        {
            var sales = await _unitOfWork.Sales.GetAllAsync();
            var saleDtos = sales.Select(sale => new SaleDTO
            {
                Id = sale.SaleID,
                UserFullName = sale.User?.FullName ?? "Unknown",
                PurchaseDate = sale.PurchaseDate,
                TotalAmount = sale.TotalAmount,
                Tickets = sale.Tickets.Select(ticket => new SimpleTicketDTO
                {
                    Id = ticket.TicketID,
                    FinalPrice = ticket.FinalPrice,
                    Status = ticket.Status
                }).ToList()
            }).ToList();
            return saleDtos;
        }

        public async Task<SaleDTO?> GetSaleByIdAsync(int id)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(id);
            if (sale is null) return null;

            var saleDto = new SaleDTO
            {
                Id = sale.SaleID,
                UserFullName = sale.User?.FullName ?? "Unknown",
                PurchaseDate = sale.PurchaseDate,
                TotalAmount = sale.TotalAmount,
                Tickets = sale.Tickets.Select(ticket => new SimpleTicketDTO
                {
                    Id = ticket.TicketID,
                    FinalPrice = ticket.FinalPrice,
                    Status = ticket.Status
                }).ToList()
            };
            return saleDto;
        }

        public async Task<IEnumerable<SaleDTO>> GetSalesByUserIdAsync(int userId)
        {
            var sales = await _unitOfWork.Sales.GetSalesByUserId(userId);

            return sales.Select(sale => new SaleDTO
            {
                Id = sale.SaleID,
                UserFullName = sale.User?.FullName ?? "Unknown",
                PurchaseDate = sale.PurchaseDate,
                TotalAmount = sale.TotalAmount,
                Tickets = sale.Tickets.Select(t => new SimpleTicketDTO
                {
                    Id = t.TicketID,
                    FinalPrice = t.FinalPrice,
                    Status = t.Status
                }).ToList()
            }).ToList();
        }
    }
}
