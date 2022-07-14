using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization ;
public class PostCode {
        [JsonPropertyName("postcode")]
        public string Code { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("admindistrict")]
        public string AdminDistrict { get {return this.Codes.AdminDistrict; } set{} }        
        [JsonPropertyName("parliamentaryconstituency")]
        public string ParliamentaryConstituency { get {return this.Codes.ParliamentaryConstituency;} set{}}


        [JsonPropertyName("area")]        
        public string Area { get {
            if(this.Latitude < 52.229466)
                return "South";
            else if(52.229466 <= this.Latitude && this.Latitude < 53.27169)
                return "Midlands";
            else if (this.Latitude >= 53.27169)
                return "North";
            else 
                return null;
        } set{} }

//        [JsonIgnore]
        [IgnoreDataMember]
        [JsonPropertyName("codes")]
        public Code Codes{get;set;}
        [JsonPropertyName("latitude")]
        internal double Latitude { get; set; }
    }
public class PostCodeModel {
        [JsonPropertyName("postcode")]
        public string PostCode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("region")]
        public string Region { get; set; }
        [JsonPropertyName("admindistrict")]
        public string AdminDistrict { get; set; }        
        [JsonPropertyName("parliamentaryconstituency")]
        public string ParliamentaryConstituency { get; set;}
        [JsonPropertyName("area")]        
        public string Area { get ;set; }
    }

    public class Code
    {
        [JsonPropertyName("admin_district")]
        public string AdminDistrict { get; set; }
        [JsonPropertyName("parliamentary_constituency")]
        public string ParliamentaryConstituency { get; set; }
    }
