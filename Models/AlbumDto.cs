namespace HollyJukeBox.Models;

public class AlbumDto
{
    public List<Images> images { get; set; }
}
public record Images(string[] types, string image);