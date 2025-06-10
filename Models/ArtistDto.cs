using System.Text.Json.Serialization;
namespace JukeBox.Models;

public class ArtistDto
{
   [JsonPropertyName("id")] 
   public string Id { get; set; }
   [JsonPropertyName("name")] 
   public string Name { get; set; }
   [JsonPropertyName("release-groups")] 
   public List<ReleaseGroup> ReleaseGroups { get; set; } = new();
   [JsonPropertyName("relations")]
   public List<Relations> Relations { get; set; } = new();
}
public record Relations(
   [property: JsonPropertyName("type")]string type, 
   [property: JsonPropertyName("url")]Url url 
);
public record ReleaseGroup(
   [property: JsonPropertyName("id")] string Id,
   [property: JsonPropertyName("title")] string Title,
   [property: JsonPropertyName("first-release-date")] string FirstReleaseDate
);

public record Url(
   [property: JsonPropertyName("id")]string id, 
   [property: JsonPropertyName("resource")]string resource
);