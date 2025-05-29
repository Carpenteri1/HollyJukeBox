using HollyJukeBox.Endpoints;
using HollyJukeBox.Models;
using HollyJukeBox.QueryModels;
using HollyJukeBox.Repository;
using HollyJukeBox.Services;
using MediatR;

namespace HollyJukeBox.Handler;

public class ArtistInfoHandler(
    IArtistEndPoint artistEndPoint,
    IMemoryCashingService memoryCashingService,
    IArtistRepository artistRepository, 
    IArtistInfoRepository artistInfoRepository, 
    IAlbumInfoRepository albumInfoRepository, 
    ICoverArtRepository coverArtRepository) 
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
            IEnumerable<AlbumInfo> albumList = null;
            if(artist.Albums is null && artist.Albums.Count == 0)
            { 
                albumList = await albumInfoRepository.GetByArtistIdAsync(artist.Mbid);
            } 
            if(artist.Albums is not null && artist.Albums.Count > 0)
            { 
                artist.Albums = albumList.ToList();
                memoryCashingService.Store($"artist:{request.Id}", artist);
                return artist;
            } 
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
            //var coverArts = await coverArtRepository.GetByIdAsync(release.Id);
            //var images = coverArts.Images;
            albums.Add(new AlbumInfo
            {
                Id = release.Id,
                ArtistInfoId = artistDto.Id,
                Title = release.Title,
                FirstReleaseDate = release.FirstReleaseDate,
                //ImageFront =  images.First(x => x.Types.Equals(ImageTypes.Front)).Image,
                //ImageBack = images.First(x => x.Types.Equals(ImageTypes.Back)).Image
            });
        }
        artist = new ArtistInfo
        {
            Mbid = artistDto.Id,
            Artist = artistDto.Name,
            Description = summary,
            Albums = albums
        };

        memoryCashingService.Store($"artist:{request.Id}", artist);
        await artistInfoRepository.SaveAsync(artist);
        await albumInfoRepository.SaveAsync(artist.Albums);
        return artist;
    }
}