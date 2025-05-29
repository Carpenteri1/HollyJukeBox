namespace HollyJukeBox.Models;

public class AlbumInfo
{
    public required string Id { get; set; }
    public required string ArtistInfoId { get; set; }
    public required string Title { get; set; }
    public required string FirstReleaseDate { get; set; }
    public string ImageFront { get; set; }
    public string ImageBack { get; set; }
}