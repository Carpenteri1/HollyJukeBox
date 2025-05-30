using HollyJukeBox.Models;

namespace HollyJukeBox.Endpoints;

public interface ICoverArtEndPoint
{
    public Task<CoverArtDto> GetById(string id);
}