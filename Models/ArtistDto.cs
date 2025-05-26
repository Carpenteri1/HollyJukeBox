using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public class ArtistDto
{
   [JsonPropertyName("id")]
   public string Id { get; set; }
   [JsonPropertyName("name")]
   public string Name { get; set; }
   
   [JsonPropertyName("release-groups")]
   public List<ReleasesGroups> ReleasesGroups { get; set; }
}

public record ReleasesGroups(
   [property: JsonPropertyName("id")] string Id,
   [property: JsonPropertyName("title")] string Title,
   [property: JsonPropertyName("first-release-date")] string FirstReleaseDate
);