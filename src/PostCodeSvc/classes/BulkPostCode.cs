using System.Text.Json.Serialization;

/// <summary>
/// Class to map the response of postcodes.io bulk api 
/// </summary>
public class BulkPostCode
{
    [JsonPropertyName("query")]
    public string Query { get; set; }
    
    [JsonPropertyName("result")]
    public PostCode Result { get; set; }
}