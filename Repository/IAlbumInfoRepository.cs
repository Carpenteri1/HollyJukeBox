using JukeBox.Models;

namespace JukeBox.Repository;

public interface IAlbumInfoRepository
{
    public Task<IEnumerable<AlbumInfo>?> GetByArtistIdAsync(string id);
    public Task<int> SaveAsync(List<AlbumInfo>? album); 
}