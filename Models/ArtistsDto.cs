using System.Text.Json.Serialization;
namespace HollyJukeBox.Models;
public record ArtistsDto(
    [property: JsonPropertyName("artists")] List<ArtistDto> Artists
);