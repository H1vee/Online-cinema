using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IValidator<TicketDTO> _validator;
    private readonly IValidator<CreateTicketDTO> _validatorCreate;

    public TicketController(ITicketService ticketService, IValidator<TicketDTO> validator, IValidator<CreateTicketDTO> validatorCreate )
    {
        _ticketService = ticketService;
        _validator = validator;
        _validatorCreate = validatorCreate;
    }

    /// <summary>
    /// Отримати всі квитки
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _ticketService.GetAllTicketsAsync();
        return Ok(tickets);
    }

    /// <summary>
    /// Отримати квиток за ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(id);
        if (ticket == null) return NotFound("Ticket not found");
        return Ok(ticket);
    }

    /// <summary>
    /// Отримати всі квитки за SaleID
    /// </summary>
    [HttpGet("sale/{saleId}")]
    public async Task<IActionResult> GetTicketsBySaleId(int saleId)
    {
        var tickets = await _ticketService.GetTicketsBySaleIdAsync(saleId);
        return Ok(tickets);
    }

    /// <summary>
    /// Створити новий квиток
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDTO ticketDto)
    {
        var validationResult = await _validatorCreate.ValidateAsync(ticketDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _ticketService.AddTicketAsync(ticketDto);
        return Ok("Ticket created successfully");
    }

    /// <summary>
    /// Видалити квиток за ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var result = await _ticketService.DeleteTicketAsync(id);
        if (!result) return NotFound("Ticket not found or cannot be deleted");
        return Ok("Ticket deleted successfully");
    }
}
