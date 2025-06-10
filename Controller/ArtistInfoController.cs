using JukeBox.QueryModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JukeBox.Controller;

[ApiController]
[Route("api/artistinfo")]
public class ArtistInfoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] string? id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var result = await mediator.Send(new ArtistInfoQuery.GetById(id));
            return result is null ? NotFound() : Ok(result);
        }
        return BadRequest("Provide either id for the artist");
    }
}