using System.Threading.Tasks;
using HollyJukeBox.Models;

namespace HollyJukeBox.Endpoints;

public interface IArtistEndPoint
{
    public Task<ArtistDto> GetById(string id);
    public Task<ArtistsDto> GetByName(string name);
}