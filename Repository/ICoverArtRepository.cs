using JukeBox.Models;

namespace JukeBox.Repository;

public interface ICoverArtRepository
{
    public Task<CoverArtDto?> GetByIdAsync(string id);
    public Task<int> SaveAsync(CoverArtDto coverArtDto);
}