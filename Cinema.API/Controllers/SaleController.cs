using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/sales")]
public class SaleController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IValidator<SaleDTO> _validator;

    public SaleController(ISaleService saleService, IValidator<SaleDTO> validator)
    {
        _saleService = saleService;
        _validator = validator;
    }

    /// <summary>
    /// Отримати всі продажі
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleService.GetAllSalesAsync();
        return Ok(sales);
    }

    /// <summary>
    /// Отримати продажу за SaleID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetSaleById(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound("Sale not found");
        return Ok(sale);
    }

    /// <summary>
    /// Отримати всі продажі користувача за UserID
    /// </summary>
    [HttpGet("user/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetSalesByUserId(int userId)
    {
        var sales = await _saleService.GetSalesByUserIdAsync(userId);
        return Ok(sales);
    }
}
