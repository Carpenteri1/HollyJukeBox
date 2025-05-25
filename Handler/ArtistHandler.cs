using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using MediatR;

namespace HollyJukeBox.Handler;

public class ArtistHandler(IArtistEndPoint artistEndPoint) : 
    IRequestHandler<ArtistQuery.GetById, ArtistDto>, 
    IRequestHandler<ArtistQuery.GetByName, ArtistDto>
{
    public async Task<ArtistDto> Handle(ArtistQuery.GetById query, CancellationToken cancellationToken) 
        => await artistEndPoint.GetById(query.Id);
    
    public async Task<ArtistDto> Handle(ArtistQuery.GetByName query, CancellationToken cancellationToken) 
        => await artistEndPoint.GetByName(query.Name);
}