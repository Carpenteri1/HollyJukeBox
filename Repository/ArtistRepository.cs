using System.Data;
using HollyJukeBox.Models;
using Dapper;

namespace HollyJukeBox.Repository;

public class ArtistRepository(IDbConnection connection) : IArtistRepository
{   
    public async Task<ArtistDto?> GetByIdAsync(string id)
    {
        var artist = await connection.QueryFirstOrDefaultAsync<ArtistDto>(
            "SELECT Id, Name FROM Artist WHERE Id = @Id", new { Id = id });
        
        if(artist is null)
        {
            return null;
        }

        var releases = await connection.QueryAsync<ReleaseGroup>(
            "SELECT Id, Title, FirstReleaseDate FROM Releases WHERE ArtistId = @Id", new { Id = id });

        var rawData = await connection.QueryAsync<(string Type, string Id, string Resource)>(
            @"SELECT rel.Type, u.Id, u.Resource
                FROM Relations rel
                JOIN Urls u ON u.Id = rel.UrlId
                WHERE rel.ArtistId = @Id", new { Id = id });

        var relations = rawData
            .Select(r => new Relations(r.Type, new Url(r.Id, r.Resource)))
            .ToList();
        
        artist.ReleaseGroups = releases.ToList();
        artist.Relations = relations.ToList();
        return artist;
    }
    
    public async Task<int> SaveAsync(ArtistDto artist)
    {
        int affectedRows = 0;

        var builder = new SqlBuilder();
        var artistInsert = builder.AddTemplate("INSERT OR IGNORE INTO Artist (Id, Name) VALUES (@Id, @Name)");
        affectedRows += await connection.ExecuteAsync(artistInsert.RawSql, new { artist.Id, artist.Name });

        foreach (var release in artist.ReleaseGroups)
        {
            var releaseBuilder = new SqlBuilder();
            var releaseInsert = releaseBuilder.AddTemplate(
                "INSERT OR IGNORE INTO Releases (Id, Title, FirstReleaseDate, ArtistId) " +
                "VALUES (@Id, @Title, @FirstReleaseDate, @ArtistId)");

            affectedRows += await connection.ExecuteAsync(releaseInsert.RawSql, new
            {
                release.Id,
                release.Title,
                release.FirstReleaseDate,
                ArtistId = artist.Id
            });
        }

        foreach (var rel in artist.Relations)
        {
            var urlBuilder = new SqlBuilder();
            var insertUrl = urlBuilder.AddTemplate("INSERT OR IGNORE INTO Urls (Id, Resource) VALUES (@Id, @Resource)");
            affectedRows += await connection.ExecuteAsync(insertUrl.RawSql, new { Id = rel.url.id, Resource = rel.url.resource });

            var relBuilder = new SqlBuilder();
            var insertRel = relBuilder.AddTemplate(
                "INSERT OR IGNORE INTO Relations (ArtistId, Type, UrlId) VALUES (@ArtistId, @Type, @UrlId)");

            affectedRows += await connection.ExecuteAsync(insertRel.RawSql, new
            {
                ArtistId = artist.Id,
                Type = rel.type,
                UrlId = rel.url.id
            });
        }

        return affectedRows;
    }
    
}