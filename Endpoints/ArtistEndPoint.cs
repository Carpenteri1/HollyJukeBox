using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class ArtistEndPoint(IOptions<ApiSettings> options, HttpClient client) : IArtistEndPoint
{
    public async Task<ArtistDto> GetById(string id) => await client.GetFromJsonAsync<ArtistDto>(
        options.Value.MusicBrainzUrl + $"artist/{id}?inc=release-groups&fmt=json");
    public async Task<ArtistsDto> GetByName(string name) => await client.GetFromJsonAsync<ArtistsDto>(
        options.Value.MusicBrainzUrl + $"artist?query={name}&fmt=json");
}