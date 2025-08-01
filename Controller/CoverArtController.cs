using JukeBox.QueryModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JukeBox.Controller;

/// <summary>
/// Handles album-related data access
/// </summary>
[ApiController]
[Route("api/coverart")]
public class CoverArtController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] string? id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var result = await mediator.Send(new CoverArtQuery.GetById(id));
            return result is null ? NotFound() : Ok(result);
        }
        
        return BadRequest("Provide id for the album");
    }
}