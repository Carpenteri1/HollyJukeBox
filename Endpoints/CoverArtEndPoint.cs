using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class CoverArtEndPoint(IOptions<ApiSettings> options, HttpClient client) : ICoverArtEndPoint
{
    public async Task<CoverArtDto> GetById(string id) => 
        await client.GetFromJsonAsync<CoverArtDto>(options.Value.CoverArtArchiveUrl + $"release-group/{id}");
}