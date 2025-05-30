using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class ArtistEndPoint(IOptions<ApiSettings> options, HttpClient client) : IArtistEndPoint
{
    public async Task<ArtistDto> GetById(string id) => await client.GetFromJsonAsync<ArtistDto>(
        options.Value.MusicBrainzUrl + $"artist/{id}?inc=release-groups+url-rels&fmt=json");
    
    public async Task<WikiDataDto> GetWikiData(string id) => await client.GetFromJsonAsync<WikiDataDto>(
            options.Value.WikiDataUrl + $"?action=wbgetentities&ids={id}&format=json&props=sitelinks");
    
    public async Task<WikipediaSummaryDto> GetWikipediaSummary(string artist) => await client.GetFromJsonAsync<WikipediaSummaryDto>(
        options.Value.WikipediaSummery + $"{artist}");
}