using System.Text.Json.Serialization;
namespace JukeBox.Models;

public record WikiDataDto(
    [property:JsonPropertyName("entities")] Dictionary<string, Entity> Entities
);
public record Entity(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("sitelinks")] Dictionary<string, SiteLink> Sitelinks 
);
public record SiteLink(
    [property: JsonPropertyName("site")] string Site,
    [property: JsonPropertyName("title")] string Title
);