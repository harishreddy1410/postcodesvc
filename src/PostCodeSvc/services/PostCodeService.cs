using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

public class PostCodeService
{    
    private static readonly HttpClient client = new HttpClient();
    private readonly postCodeApiUrl = "https://api.postcodes.io/postcodes/";
    public async Task<PostCode> PostCodeLookup(string postCode)
    {
        string url = postCodeApiUrl + postCode;        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>
        (await result.Content.ReadAsStringAsync());
        return jsonData.ContainsKey("result") ? JsonSerializer.Deserialize<PostCode>(jsonData["result"].ToString()) : new PostCode();   
    }

    public async Task<List<PostCode>> AutoComplete(string postCode)
    {
        string url = postCodeApiUrl + postCode + "/autocomplete";        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(await result.Content.ReadAsStringAsync());
        List<PostCode> searchResult = new List<PostCode>();
        if(jsonData["result"] != null)
        {
            //Make bulk lookup
            var payload = "{\"postcodes\":"+ jsonData["result"].ToString() + "}";
            HttpContent content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(postCodeApiUrl,content);
            var bulkJson = JsonSerializer.Deserialize<Dictionary<string, object>>(await resp.Content.ReadAsStringAsync());
            if(bulkJson.ContainsKey("result"))
            {
                System.Console.WriteLine("-----------PARSING BULK UPLOADS------");
                var finalResult = JsonSerializer.Deserialize<List<BulkPostCode>>(bulkJson["result"].ToString());
                finalResult.ForEach(x=>{
                    searchResult.Add(x.Result);
                });
            }
        }        
        return searchResult;
    }    
}