using System.Text.Json.Serialization;

/// <summary>
/// Dto - pass only the required fields to the consumer/api caller from the result
/// </summary>
public class PostCodeDto
{

    [JsonPropertyName("postcode")]
    public string PostCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("admindistrict")]
    public string AdminDistrict { get; set; }

    [JsonPropertyName("parliamentaryconstituency")]
    public string ParliamentaryConstituency { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }
}

