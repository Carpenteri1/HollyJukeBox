using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class AlbumEndPoint(IOptions<ApiSettings> options, HttpClient client) : IAlbumEndPoint
{
    public async Task<AlbumDto> GetById(string id) => 
        await client.GetFromJsonAsync<AlbumDto>(options.Value.CoverArtArchiveUrl + $"release-group/{id}");
}