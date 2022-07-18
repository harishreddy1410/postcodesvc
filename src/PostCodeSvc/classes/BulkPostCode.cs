using System.Text.Json.Serialization;

/// <summary>
/// Class to map the response of postcodes.io bulk api 
/// </summary>
public class BulkPostCode
{
    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("query")]
    public string Query { get; set; }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("result")]
    public PostCodeDetail Result { get; set; }
}