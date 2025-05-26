using System.Text.Json.Serialization;

namespace HollyJukeBox.Models;

public class WikipediaSummaryDto
{
        [JsonPropertyName("extract")] 
        public string Extract { get; set; }
}