using System.Threading.Tasks;
using Cinema.Core.DTOs;
using Cinema.Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/actors")]
public class ActorController : ControllerBase
{
    private readonly IActorService _actorService;
    private readonly IValidator<CreateActorDTO> _validator;

    public ActorController(IActorService actorService, IValidator<CreateActorDTO> validator)
    {
        _actorService = actorService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActors()
    {
        var actors = await _actorService.GetAllActorsAsync();
        return Ok(actors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActorById(int id)
    {
        var actor = await _actorService.GetActorByIdAsync(id);
        if (actor == null) return NotFound("Actor not found");
        return Ok(actor);
    }

    [HttpPost]
    public async Task<IActionResult> CreateActor([FromBody] CreateActorDTO actorDto)
    {
        var validationResult = await _validator.ValidateAsync(actorDto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        await _actorService.AddActorAsync(actorDto);
        return Ok("Actor created successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int id)
    {
        var result = await _actorService.DeleteActorAsync(id);
        if (!result) return NotFound("Actor not found or cannot be deleted");
        return Ok("Actor deleted successfully");
    }
}