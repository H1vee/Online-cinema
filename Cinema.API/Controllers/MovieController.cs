using System.Collections.Generic;
using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
	private readonly IMovieService _movieService;
	private readonly IValidator<MovieDTO> _validator;

	public MovieController(IMovieService movieService, IValidator<MovieDTO> validator)
	{
		_movieService = movieService;
		_validator = validator;
	}

	/// <summary>
	/// Отримати всі фільми
	/// </summary>
	[HttpGet]
	public async Task<IActionResult> GetAllMovies()
	{
		var movies = await _movieService.GetAllMoviesAsync();
		return Ok(movies);
	}

	/// <summary>
	/// Отримати фільм за ID
	/// </summary>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetMovieById(int id)
	{
		var movie = await _movieService.GetMovieByIdAsync(id);
		if (movie == null) return NotFound("Movie not found");
		return Ok(movie);
	}

	/// <summary>
	/// Отримати фільми за жанром
	/// </summary>
	[HttpGet("genre/{genre}")]
	public async Task<IActionResult> GetMoviesByGenre(string genre)
	{
		var movies = await _movieService.GetMoviesByGenreAsync(genre);
		return Ok(movies);
	}

	/// <summary>
	/// Додати новий фільм
	/// </summary>
	[HttpPost]
	public async Task<IActionResult> CreateMovie([FromBody] MovieDTO movieDto)
	{
		var validationResult = await _validator.ValidateAsync(movieDto);
		if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

		await _movieService.AddMovieAsync(movieDto);
		return Ok("Movie created successfully");
	}

	/// <summary>
	/// Видалити фільм за ID
	/// </summary>
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMovie(int id)
	{
		var result = await _movieService.DeleteMovieAsync(id);
		if (!result) return NotFound("Movie not found or cannot be deleted");
		return Ok("Movie deleted successfully");
	}
}
