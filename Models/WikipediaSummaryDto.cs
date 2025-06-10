using System.Text.Json.Serialization;

namespace JukeBox.Models;

public record WikipediaSummaryDto
(
    [property: JsonPropertyName("extract")]  string Extract,
    string ArtsistId
);