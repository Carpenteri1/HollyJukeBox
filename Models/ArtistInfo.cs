namespace HollyJukeBox.Models;

public class ArtistInfo
{
    public required string Mbid { get; set; }
    public required string Name { get; set; }
    public required List<AlbumInfo> Albums { get; set; }
    public required string Description { get; set; }
}