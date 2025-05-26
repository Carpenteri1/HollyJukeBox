using HollyJukeBox.Models;

namespace HollyJukeBox.Endpoints;

public interface IAlbumEndPoint
{
    public Task<AlbumDto> GetById(string id);
}