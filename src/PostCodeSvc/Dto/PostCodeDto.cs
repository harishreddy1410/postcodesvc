using System.Text.Json.Serialization;

/// <summary>
/// Dto - pass only the required fields to the consumer/api caller from the result
/// </summary>
public class PostCodeDto
{
    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("postcode")]
    public string PostCode { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("region")]
    public string Region { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("admindistrict")]
    public string AdminDistrict { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("parliamentaryconstituency")]
    public string ParliamentaryConstituency { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("area")]
    public string Area { get; set; }
}

