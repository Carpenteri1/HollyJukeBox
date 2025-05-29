using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using HollyJukeBox.Repository;
using HollyJukeBox.Services;
using MediatR;

namespace HollyJukeBox.Handler;

public class ArtistHandler(
    IArtistEndPoint artistEndPoint, 
    IMemoryCashingService memoryCashingService,
    IArtistRepository artistRepository) : 
    IRequestHandler<ArtistQuery.GetById, ArtistDto>, 
    IRequestHandler<ArtistQuery.GetWikiData, WikiDataDto>,
    IRequestHandler<ArtistQuery.GetWikipediaSummary, WikipediaSummaryDto>
{
    public async Task<ArtistDto> Handle(ArtistQuery.GetById request, CancellationToken cancellationToken)
    {
        var artist = memoryCashingService.Get<ArtistDto>(request.Id);
        
        if (artist is not null)
        {
            return artist;
        }

        artist = await artistRepository.GetByIdAsync(request.Id);
        if (artist is not null)
        {
            memoryCashingService.Store(request.Id, artist);
            return artist;
        }
        
        artist = await artistEndPoint.GetById(request.Id);
        memoryCashingService.Store(request.Id, artist);
        await artistRepository.SaveAsync(artist);
        return artist;
    }

    public async Task<WikiDataDto> Handle(ArtistQuery.GetWikiData request, CancellationToken cancellationToken) 
        => await artistEndPoint.GetWikiData(request.id);
    public async Task<WikipediaSummaryDto> Handle(ArtistQuery.GetWikipediaSummary request, CancellationToken cancellationToken) 
        => await artistEndPoint.GetWikipediaSummary(request.enwikiTitle);
}