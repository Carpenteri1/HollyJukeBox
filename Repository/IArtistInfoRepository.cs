using HollyJukeBox.Models;

namespace HollyJukeBox.Repository;

public interface IArtistInfoRepository
{
    public Task<ArtistInfo?> GetByIdAsync(string id);
    public Task<int> SaveAsync(ArtistInfo artist); 
}