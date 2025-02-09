using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleService.GetAllSalesAsync();
        return Ok(sales);
    }

    /// <summary>
    /// Отримати продаж за ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound("Sale not found");
        return Ok(sale);
    }

    /// <summary>
    /// Створити нову продажу
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] SaleDTO saleDto)
    {
        var validationResult = await _validator.ValidateAsync(saleDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _saleService.AddSaleAsync(saleDto);
        return Ok("Sale created successfully");
    }

    /// <summary>
    /// Видалити продажу за ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var result = await _saleService.DeleteSaleAsync(id);
        if (!result) return NotFound("Sale not found or cannot be deleted");
        return Ok("Sale deleted successfully");
    }
}
