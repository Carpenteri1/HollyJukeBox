using HollyJukeBox.QueryModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HollyJukeBox.Controller;

/// <summary>
/// Handles artist-related data access
/// </summary>
[ApiController]
[Route("api/artist")]
public class ArtistController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? id, [FromQuery] string? name)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var result = await mediator.Send(new ArtistQuery.GetById(id));
            return result is null ? NotFound() : Ok(result);
        }
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            var result = await mediator.Send(new ArtistQuery.GetByName(name));
            return result is null ? NotFound() : Ok(result);
        }
        return BadRequest("Provide either id or name for the artist");
    }
}