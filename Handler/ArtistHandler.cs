using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using MediatR;

namespace HollyJukeBox.Handler;

public class ArtistHandler(IArtistEndPoint artistEndPoint) : 
    IRequestHandler<ArtistQuery.GetById, ArtistDto>, 
    IRequestHandler<ArtistQuery.GetByName, ArtistDto>,
    IRequestHandler<ArtistQuery.GetWikiData, WikiDataDto>,
    IRequestHandler<ArtistQuery.GetWikipediaSummary, WikipediaSummaryDto>
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

    public async Task<WikiDataDto> Handle(ArtistQuery.GetWikiData request, CancellationToken cancellationToken) 
        => await artistEndPoint.GetWikiData(request.id);
    public async Task<WikipediaSummaryDto> Handle(ArtistQuery.GetWikipediaSummary request, CancellationToken cancellationToken) 
        => await artistEndPoint.GetWikipediaSummary(request.artist);
}