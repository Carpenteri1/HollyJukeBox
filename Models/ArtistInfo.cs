namespace HollyJukeBox.Models;

public class ArtistInfo
{
    public string Mbid { get; set; }
    public string Artist { get; set; }
    public List<AlbumInfo> Albums { get; set; }
    public string Description { get; set; }
}