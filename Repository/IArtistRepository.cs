using JukeBox.Models;

namespace JukeBox.Repository;

public interface IArtistRepository
{ 
        public Task<ArtistDto?> GetByIdAsync(string id); 
        public Task<int> SaveAsync(ArtistDto artistDto); 
}