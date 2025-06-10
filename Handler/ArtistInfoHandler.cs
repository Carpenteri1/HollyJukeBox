using JukeBox.Endpoints;
using JukeBox.Models;
using JukeBox.QueryModels;
using JukeBox.Repository;
using JukeBox.Services;
using MediatR;

namespace JukeBox.Handler;

public class ArtistInfoHandler(
    IArtistEndPoint artistEndPoint,
    ICoverArtEndPoint coverArtEndPoint,
    IMemoryCashingService memoryCashingService,
    IArtistRepository artistRepository, 
    IArtistInfoRepository artistInfoRepository, 
    IAlbumInfoRepository albumInfoRepository, 
    ICoverArtRepository coverArtRepository,
    IRetryPolicyService retryPolicy) 
    : IRequestHandler<ArtistInfoQuery.GetById, ArtistInfo>
{
    public async Task<ArtistInfo> Handle(ArtistInfoQuery.GetById request, CancellationToken cancellationToken)
    {
        var artist = memoryCashingService.Get<ArtistInfo>($"artist:{request.Id}");
        if (artist is not null)
        {
            return artist;
        }
        
        artist = await artistInfoRepository.GetByIdAsync(request.Id);
        if (artist is not null)
        {
            var albumList = await albumInfoRepository.GetByArtistIdAsync(request.Id);
            if(albumList is not null && albumList.ToList().Count > 0)
            { 
                artist.Albums = albumList.ToList();
                memoryCashingService.Store($"artist:{request.Id}", artist);
                return artist;
            }

            return null;
        }

        var summary = string.Empty;
        var artistDto = memoryCashingService.Get<ArtistDto>($"artistDto:{request.Id}");
        if (artistDto is null)
        {
            artistDto = await artistRepository.GetByIdAsync(request.Id);
            if (artistDto is null)
            {
                artistDto = await artistEndPoint.GetById(request.Id);
                var wikiSummaryId = string.Empty;
                var relation = artistDto.Relations
                    .Find(x => Enum.TryParse<KeyTypes>(x.type, true, out var keyType) && keyType == KeyTypes.wikipedia);
                if(relation is not null)//if relation has wikipedia 
                {
                    wikiSummaryId = relation.url.resource.Split('/').Last();
                }
                else
                {
                    relation = artistDto.Relations
                        .Find(x => Enum.TryParse<KeyTypes>(x.type, true, out var keyType) && keyType == KeyTypes.wikidata);            
                    var wikiId = relation.url.resource.Split('/').Last();
                    var wikiData = await artistEndPoint.GetWikiData(wikiId);
                    var sitelinks = wikiData.Entities.First().Value.Sitelinks;
                    wikiSummaryId = sitelinks.First(x => 
                        Enum.TryParse<KeyTypes>(x.Key, true, out var keyType) && keyType == KeyTypes.enwiki).Value.Title;           
                }
        
                var wikiSummary = await artistEndPoint.GetWikipediaSummary(wikiSummaryId);
                summary = wikiSummary.Extract;
                memoryCashingService.Store($"artistDto:{request.Id}", artistDto);
            }
        }
        var albums = new List<AlbumInfo>();
        
        foreach (var release in artistDto.ReleaseGroups)
        {
            var coverArt = memoryCashingService.Get<CoverArtDto>($"coverArt:{release.Id}");
            if(coverArt is null) coverArt = await coverArtRepository.GetByIdAsync(release.Id);
            if(coverArt is null)
            {
                coverArt = await retryPolicy.RetryGet().ExecuteAsync(() => coverArtEndPoint.GetById(release.Id));
                if(coverArt is not null)
                {
                    var sortedImages = coverArt.Images
                        .Where(x =>
                            x.Types.Contains(nameof(ImageTypes.Front)) ||
                            x.Types.Contains(nameof(ImageTypes.Back)));

                    coverArt.Images = sortedImages.ToList();
                    coverArt.Id = release.Id;
                }
            }
            
            albums.Add(new AlbumInfo
            {
                Id = release.Id,
                ArtistInfoId = artistDto.Id,
                Title = release.Title,
                FirstReleaseDate = release.FirstReleaseDate,
                ImageFront = coverArt.Images.FirstOrDefault(x => x.Types.Contains(nameof(ImageTypes.Front)))?.Image ?? string.Empty,
                ImageBack = coverArt.Images.FirstOrDefault(x => x.Types.Contains(nameof(ImageTypes.Back)))?.Image ?? string.Empty
            });
            memoryCashingService.Store($"coverArt:{release.Id}", coverArt);
            await coverArtRepository.SaveAsync(coverArt);
        }
        artist = new ArtistInfo
        {
            Mbid = artistDto.Id,
            Name = artistDto.Name,
            Description = summary,
            Albums = albums
        };

        memoryCashingService.Store($"artist:{request.Id}", artist);
        await artistInfoRepository.SaveAsync(artist);
        await albumInfoRepository.SaveAsync(artist.Albums);
        return artist;
    }
}