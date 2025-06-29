using System.Text.Json.Serialization;

public class SerpApiResponse
{
    [JsonPropertyName("organic_results")]
    public List<OrganicResult> OrganicResults { get; set; }
}

public class OrganicResult
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }
}