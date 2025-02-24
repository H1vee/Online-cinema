using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/showtimes")]
public class ShowtimeController : ControllerBase
{
    private readonly IShowtimeService _showtimeService;
    private readonly IValidator<ShowtimeDTO> _validator;
    private readonly IValidator<CreateShowtimeDTO> _validatorCreate;

    public ShowtimeController(IShowtimeService showtimeService, IValidator<ShowtimeDTO> validator,IValidator<CreateShowtimeDTO> validatorCreate )
    {
        _showtimeService = showtimeService;
        _validator = validator;
        _validatorCreate = validatorCreate;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllShowtimes()
    {
        var showtimes = await _showtimeService.GetAllShowtimesAsync();
        return Ok(showtimes);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetShowtimeById(int id)
    {
        var showtime = await _showtimeService.GetShowtimeByIdAsync(id);
        if (showtime == null) return NotFound("Showtime not found");
        return Ok(showtime);
    }

    [HttpGet("by-date")]
    [Authorize]
    public async Task<IActionResult> GetShowtimesByDate([FromQuery] DateTime date)
    {
        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        var showtimes = await _showtimeService.GetShowtimesByDateAsync(date);
        return Ok(showtimes);
    }

    [HttpGet("by-genre")]
    [Authorize]
    public async Task<IActionResult> GetShowtimesByGenre([FromQuery] string genreName)
    {
        var showtimes = await _showtimeService.GetShowtimesByGenreAsync(genreName);
        return Ok(showtimes);
    }

    [HttpGet("by-duration")]
    [Authorize]
    public async Task<IActionResult> GetShowtimesByDuration([FromQuery] int maxDuration)
    {
        if (maxDuration <= 0)
        {
            return BadRequest("Duration must be greater than 0.");
        }

        var showtimes = await _showtimeService.GetShowtimesByDurationAsync(maxDuration);
        return Ok(showtimes);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateShowtime([FromBody] CreateShowtimeDTO showtimeDto)
    {
        var validationResult = await _validatorCreate.ValidateAsync(showtimeDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _showtimeService.AddShowtimeAsync(showtimeDto);
        return Ok("Showtime created successfully");
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteShowtime(int id)
    {
        var result = await _showtimeService.DeleteShowtimeAsync(id);
        if (!result) return NotFound("Showtime not found or cannot be deleted");
        return Ok("Showtime deleted successfully");
    }
}
