using System.Data;
using Dapper;
using HollyJukeBox.Models;

namespace HollyJukeBox.Repository;

public class AlbumInfoRepository(IDbConnection connection) : IAlbumInfoRepository
{
    public async Task<IEnumerable<AlbumInfo>?> GetByArtistIdAsync(string id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * FROM AlbumInfo /**where**/");
        builder.Where("ArtistInfoId = @Id");
        return await connection.QueryAsync<AlbumInfo>(template.RawSql, new { Id = id });
    }

    public async Task<int> SaveAsync(List<AlbumInfo>? albums)
    {
        var rowsAffected = 0;
        foreach (var album in albums)
        {
            var builder = new SqlBuilder();
            var artistInsert = 
                builder.AddTemplate("INSERT OR IGNORE INTO AlbumInfo (Id, Title, FirstReleaseDate, ImageFront, ImageBack, ArtistInfoId) " +
                                    "VALUES (@Id, @Title, @FirstReleaseDate, @ImageFront, @ImageBack, @ArtistInfoId)"); 
           rowsAffected += await connection.ExecuteAsync(artistInsert.RawSql, 
                new
                {
                    album.Id, 
                    album.Title,
                    album.FirstReleaseDate, 
                    album.ImageFront, 
                    album.ImageBack,
                    album.ArtistInfoId
                });   
        }

        return rowsAffected;
    }
}