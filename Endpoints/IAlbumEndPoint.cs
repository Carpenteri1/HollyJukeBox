using System.Threading.Tasks;
using HollyJukeBox.QueryModels;

namespace HollyJukeBox.Endpoints;

public interface IAlbumEndPoint
{
    public Task<AlbumQuery> GetById();
}