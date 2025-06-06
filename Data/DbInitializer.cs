using Dapper;
using Microsoft.Data.Sqlite;

namespace HollyJukeBox.Data;

public class DbInitializer
{
    private SqliteConnection connection;

    public DbInitializer(IConfiguration configuration)
    {
        connection = new SqliteConnection(configuration.GetConnectionString("HollyJukeBoxDb"));
    }
    
    public async Task EnsureTablesCreatedAsync()
    {
        await connection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Artist (
                Id TEXT PRIMARY KEY,
                Name TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Releases (
                Id TEXT PRIMARY KEY,
                Title TEXT NOT NULL,
                FirstReleaseDate TEXT,
                ArtistId TEXT NOT NULL,
                FOREIGN KEY (ArtistId) REFERENCES Artist(Id)
            );
            
           CREATE TABLE IF NOT EXISTS Relations (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ArtistId TEXT NOT NULL,
                Type TEXT NOT NULL,
                UrlId TEXT NOT NULL,
                FOREIGN KEY (ArtistId) REFERENCES Artist(Id),
                FOREIGN KEY (UrlId) REFERENCES Urls(Id)
            );

            CREATE TABLE IF NOT EXISTS Urls (
                Id TEXT PRIMARY KEY,
                Resource TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS CoverArt (
                Id TEXT PRIMARY KEY
            );

            CREATE TABLE IF NOT EXISTS Images (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CoverArtId TEXT NOT NULL,
                Image TEXT NOT NULL,
                FOREIGN KEY (CoverArtId) REFERENCES CoverArt(Id)
            );

            CREATE TABLE IF NOT EXISTS ImageTypes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ImageId INTEGER NOT NULL,
                Type TEXT NOT NULL,
                FOREIGN KEY (ImageId) REFERENCES Images(Id)
            );

            CREATE TABLE IF NOT EXISTS ArtistInfo (
                Id TEXT PRIMARY KEY,
                Name TEXT NOT NULL,
                Description TEXT
            );

            CREATE TABLE IF NOT EXISTS AlbumInfo (
                Id TEXT PRIMARY KEY,
                Title TEXT NOT NULL,
                FirstReleaseDate TEXT,
                ImageFront TEXT,
                ImageBack TEXT,
                ArtistInfoId TEXT NOT NULL,
                FOREIGN KEY (ArtistInfoId) REFERENCES ArtistInfo(Id)
            );
        ");
    }
}