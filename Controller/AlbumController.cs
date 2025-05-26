using HollyJukeBox.QueryModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HollyJukeBox.Controller;

public class AlbumController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var result = await mediator.Send(new AlbumQuery.GetById(id));
            return result is null ? NotFound() : Ok(result);
        }
        
        return BadRequest("Provide id.");
    }
}