using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public class WikiDataDto
{
    [JsonPropertyName("entities")]
    public Dictionary<string, Entity> Entities { get; set; }
}
public record Entity(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("sitelinks")] Dictionary<string, SiteLink> Sitelinks 
);
public record SiteLink(
    [property: JsonPropertyName("site")] string site,
    [property: JsonPropertyName("title")] string title
);

public record Enwiki(
    [property: JsonPropertyName("site")] string enwiki
);