using System.Data;
using JukeBox.Models;
using Dapper;

namespace JukeBox.Repository;

public class CoverArtRepository(IDbConnection connection) : ICoverArtRepository
{   
    public async Task<CoverArtDto?> GetByIdAsync(string id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT c.* FROM CoverArt c /**join**/ /**leftjoin**/ /**where**/ LIMIT 1");
        builder.LeftJoin("Images i ON i.CoverArtId = c.Id");
        builder.LeftJoin("ImageTypes it ON it.ImageId = i.Id");
        builder.Where("c.Id = @Id", new { Id = id });
        
        var covertArt =  await connection.QueryFirstOrDefaultAsync<CoverArtDto>(template.RawSql, template.Parameters);
        return covertArt;
    }

    public async Task<int> SaveAsync(CoverArtDto coverArtDto)
    {
        int affectedRows = 0;
        var builder = new SqlBuilder();
        var insertCoverArt = builder.AddTemplate("INSERT OR IGNORE INTO CoverArt (Id) VALUES (@Id)");
        affectedRows += await connection.ExecuteAsync(insertCoverArt.RawSql, new { Id = coverArtDto.Id });

        foreach (var image in coverArtDto.Images)
        {
            var insertImage = "INSERT INTO Images (CoverArtId, Image) VALUES (@CoverArtId, @Image); SELECT last_insert_rowid();";
            var imageId = await connection.ExecuteScalarAsync<long>(insertImage, new { CoverArtId = coverArtDto.Id, Image = image.Image });

            foreach (var type in image.Types)
            {
                var builderType = new SqlBuilder();
                var insertType = builderType.AddTemplate("INSERT INTO ImageTypes (ImageId, Type) VALUES (@ImageId, @Type)");
                affectedRows += await connection.ExecuteAsync(insertType.RawSql, new { ImageId = imageId, Type = type });
            }

            affectedRows++; 
        }

        return affectedRows;
    }
}