using HollyJukeBox.QueryModels;

namespace HollyJukeBox.Endpoints;

public interface IAlbumEndPoint
{
    public Task<AlbumQuery> GetById();
}