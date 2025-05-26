using System;
using System.Threading.Tasks;
using HollyJukeBox.QueryModels;

namespace HollyJukeBox.Endpoints;

public class AlbumEndPoint : IAlbumEndPoint
{
    public Task<AlbumQuery> GetById()
    {
        throw new NotImplementedException();
    }
}