using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IValidator<CreateGenreDTO> _validator;

    public GenreController(IGenreService genreService, IValidator<CreateGenreDTO> validator)
    {
        _genreService = genreService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);
        if (genre == null) return NotFound("Genre not found");
        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO genreDto)
    {
        var validationResult = await _validator.ValidateAsync(genreDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _genreService.AddGenreAsync(genreDto);
        return Ok("Genre created successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var result = await _genreService.DeleteGenreAsync(id);
        if (!result) return NotFound("Genre not found or cannot be deleted");
        return Ok("Genre deleted successfully");
    }
}