using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public record WikipediaSummaryDto
(
    [property: JsonPropertyName("extract")]  string Extract,
    string ArtsistId
);