using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;
public record CoverArtDto(
    string Id, 
    [property:JsonPropertyName("images")] List<Images> Images
);
public record Images(
    [property: JsonPropertyName("types")] string[] Types,
    [property: JsonPropertyName("image")] string Image
);
