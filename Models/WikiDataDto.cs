using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public class WikiDataDto
{
    [JsonPropertyName("entities")]
    public Dictionary<string, Entity> Entities { get; set; }
}
public record Entity(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("sitelinks")] List<SiteLink> SiteLinks
);
public record SiteLink(
    [property: JsonPropertyName("enwiki")] Enwiki Enwiki
);

public record Enwiki(
    [property: JsonPropertyName("site")] string enwiki
);