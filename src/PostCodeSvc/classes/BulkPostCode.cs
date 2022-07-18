using System.Text.Json.Serialization;

public class BulkPostCode
{
    [JsonPropertyName("query")]
    public string Query { get; set; }
    
    [JsonPropertyName("result")]
    public PostCode Result { get; set; }
}