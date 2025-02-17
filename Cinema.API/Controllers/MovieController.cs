using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IValidator<CreateMovieDTO> _validator;

    public MovieController(IMovieService movieService, IValidator<CreateMovieDTO> validator)
    {
        _movieService = movieService;
        _validator = validator;
    }

    /// <summary>
    /// Отримати всі фільми
    /// </summary>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    /// <summary>
    /// Отримати фільм за ID
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null) return NotFound("Movie not found.");
        return Ok(movie);
    }

    /// <summary>
    /// Отримати актуальні фільми
    /// </summary>
    [HttpGet("recent")]
    [Authorize]
    public async Task<IActionResult> GetRecentMovies()
    {
        var movies = await _movieService.GetRecentMoviesAsync();
        return Ok(movies);
    }

    /// <summary>
    /// Отримати нові фільми
    /// </summary>
    [HttpGet("upcoming")]
    [Authorize]
    public async Task<IActionResult> GetUpcomingMovies()
    {
        var movies = await _movieService.GetUpcomingMoviesAsync();
        return Ok(movies);
    }

    /// <summary>
    /// Додати новий фільм
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDTO movieDto)
    {
        var validationResult = await _validator.ValidateAsync(movieDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _movieService.AddMovieAsync(movieDto);
        return Ok("Movie created successfully.");
    }

    /// <summary>
    /// Видалити фільм за ID
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var result = await _movieService.DeleteMovieAsync(id);
        if (!result) return NotFound("Movie not found.");
        return Ok("Movie deleted successfully.");
    }
}
