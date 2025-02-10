using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/showtimes")]
public class ShowtimeController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;
    private readonly IValidator<ShowtimeDTO> _validator;

    public ShowtimeController(IShowtimeService showtimeService, IValidator<ShowtimeDTO> validator)
    {
        _showtimeService = showtimeService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllShowtimes()
    {
        var showtimes = await _showtimeService.GetAllShowtimesAsync();
        return Ok(showtimes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShowtimeById(int id)
    {
        var showtime = await _showtimeService.GetShowtimeByIdAsync(id);
        if (showtime == null) return NotFound("Showtime not found");
        return Ok(showtime);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingShowtimes()
    {
        var showtimes = await _showtimeService.GetUpcomingShowtimesAsync();
        return Ok(showtimes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShowtime([FromBody] ShowtimeDTO showtimeDto)
    {
        var validationResult = await _validator.ValidateAsync(showtimeDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _showtimeService.AddShowtimeAsync(showtimeDto);
        return Ok("Showtime created successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShowtime(int id)
    {
        var result = await _showtimeService.DeleteShowtimeAsync(id);
        if (!result) return NotFound("Showtime not found or cannot be deleted");
        return Ok("Showtime deleted successfully");
    }
}
