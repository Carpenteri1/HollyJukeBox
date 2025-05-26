using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public class ArtistsDto
{
    [JsonPropertyName("artists")]
    public List<ArtistDto> Artists { get; set; }
}