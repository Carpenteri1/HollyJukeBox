using System.Net;
using HollyJukeBox.Models;
using HollyJukeBox.Services;
using Microsoft.Extensions.Options;

namespace HollyJukeBox.Endpoints;

public class CoverArtEndPoint(IOptions<ApiSettings> options, HttpClient client) : ICoverArtEndPoint
{
    public async Task<CoverArtDto> GetById(string id)
    {
        var response = await client.GetAsync(options.Value.CoverArtArchiveUrl + $"release-group/{id}");
        if (response.StatusCode != HttpStatusCode.OK)
        {
            return new CoverArtDto{
                Id = id,
                Images = new List<Images>()
            };
        }

        return await response.Content.ReadFromJsonAsync<CoverArtDto>();
    }
}