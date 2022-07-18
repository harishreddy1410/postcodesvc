using System.Text.Json.Serialization;
/// <summary>
/// Used to map the postcode.io api response
/// </summary>
public class PostCodeDetail 
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
    public string AdminDistrict { get {return this.Codes.AdminDistrict; } set{} }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("parliamentaryconstituency")]
    public string ParliamentaryConstituency { get {return this.Codes.ParliamentaryConstituency;} set{}}

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("area")]        
    public string Area { get {
        System.Console.WriteLine(" Latitude value : " + this.Latitude);
        if(this.Latitude < 52.229466)
            return "South";
        else if(52.229466 <= this.Latitude && this.Latitude < 53.27169)
            return "Midlands";
        else if (this.Latitude >= 53.27169)
            return "North";
        else 
            return null;
    } set{} }

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("codes")]
    public Code Codes{get;set;}

    /// <summary>
    /// Mapping property for response from postcode.io api 
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
}

/// <summary>
/// Used to map the postcode.io api response
/// </summary>
public class Code
{
    [JsonPropertyName("admin_district")]
    public string AdminDistrict { get; set; }
    
    [JsonPropertyName("parliamentary_constituency")]
    public string ParliamentaryConstituency { get; set; }
}
