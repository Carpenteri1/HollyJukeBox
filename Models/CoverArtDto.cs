using System.Text.Json.Serialization;

namespace JukeBox.Models;

public class CoverArtDto
{
    public string Id { get; set; }
    [JsonPropertyName("images")] 
    public List<Images> Images { get; set; } 
}

public record Images(
    [property: JsonPropertyName("types")] string[] Types,
    [property: JsonPropertyName("image")] string Image
);
