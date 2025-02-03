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
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }

        public async Task<SaleDTO?> GetSaleByIdAsync(int id)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(id);
            return sale is null ? null : _mapper.Map<SaleDTO>(sale);
        }

        public async Task AddSaleAsync(SaleDTO saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            await _unitOfWork.Sales.AddAsync(sale);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            var sale = await _unitOfWork.Sales.GetByIdAsync(id);
            if (sale is null)
            {
                return false;
            }

            _unitOfWork.Sales.Remove(sale);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<SaleDTO>> GetSalesByUserIdAsync(int userId)
        {
            var sales = await _unitOfWork.Sales.GetSalesByUserId(userId);
            return _mapper.Map<IEnumerable<SaleDTO>>(sales);
        }
    }
}
