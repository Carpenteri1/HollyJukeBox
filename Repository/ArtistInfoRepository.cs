using System.Data;
using Dapper;
using HollyJukeBox.Models;

namespace HollyJukeBox.Repository;

public class ArtistInfoRepository(IDbConnection connection) : IArtistInfoRepository
{
    public async Task<ArtistInfo> GetByIdAsync(string id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * FROM ArtistInfo /**where**/ LIMIT 1");
        builder.Where("id = @Id", new { Id = id });
        return await connection.QueryFirstOrDefaultAsync<ArtistInfo>(template.RawSql, 
            new { Id = id });
    }

    public async Task<int> SaveAsync(ArtistInfo artist)
    {
        var builder = new SqlBuilder();
        var artistInsert = 
            builder.AddTemplate("INSERT OR IGNORE INTO ArtistInfo (Id, Name, Description) " +
                                "VALUES (@Id, @Name, @Description)"); 
        
        return await connection.ExecuteAsync(artistInsert.RawSql, 
            new
            {
                artist.Mbid, 
                artist.Artist,
                artist.Description
            });
    }
}