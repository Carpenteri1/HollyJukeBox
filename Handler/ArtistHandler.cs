using System.Threading;
using System.Threading.Tasks;
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
    {
        var result = await artistEndPoint.GetByName(query.Name);
        var artist = result.Artists.First(x => string.Equals(x.Name, query.Name, StringComparison.OrdinalIgnoreCase));
        artist = await artistEndPoint.GetById(artist.Id);
        return artist; 
    }
    
}