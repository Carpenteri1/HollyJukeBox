using JukeBox.Models;

namespace JukeBox.Endpoints;

public interface ICoverArtEndPoint
{
    public Task<CoverArtDto> GetById(string id);
}