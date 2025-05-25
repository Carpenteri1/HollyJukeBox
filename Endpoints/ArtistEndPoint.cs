using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class ArtistEndPoint(IOptions<ApiSettings> options, HttpClient client) : IArtistEndPoint
{
    private readonly string artistRequestUrl = options.Value.MusicBrainzUrl+"artist";
    public async Task<ArtistDto> GetById(string id) => await client.GetFromJsonAsync<ArtistDto>(
        artistRequestUrl + $"/{id}?inc=release-groups&fmt=json");
    public async Task<ArtistDto> GetByName(string name)
    {
        var result = await client.GetFromJsonAsync<ArtistsDto>(
            artistRequestUrl + $"?query={name}&fmt=json");
        return result.Artists.First(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}