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
    /// Îòðèìàòè âñ³ ïðîäàæ³
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleService.GetAllSalesAsync();
        return Ok(sales);
    }

    /// <summary>
    /// Îòðèìàòè ïðîäàæ çà ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null) return NotFound("Sale not found");
        return Ok(sale);
    }

    /// <summary>
    /// Ñòâîðèòè íîâó ïðîäàæó
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
    /// Отримати всі продажі користувача за UserID
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetSalesByUserId(int userId)
    {
        var sales = await _saleService.GetSalesByUserIdAsync(userId);
        return Ok(sales);
    }
    /// <summary>
    /// Âèäàëèòè ïðîäàæó çà ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var result = await _saleService.DeleteSaleAsync(id);
        if (!result) return NotFound("Sale not found or cannot be deleted");
        return Ok("Sale deleted successfully");
    }
}
