using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

public class PostCodeService
{
    
    private static readonly HttpClient client = new HttpClient();
    public async Task<PostCode> PostCodeLookup(string postCode)
    {
        string url = "https://api.postcodes.io/postcodes/" + postCode;        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>
        (await result.Content.ReadAsStringAsync());
        return jsonData.ContainsKey("result") ? JsonSerializer.Deserialize<PostCode>(jsonData["result"].ToString()) : new PostCode();   
    }

    public async Task<List<PostCode>> AutoComplete(string postCode)
    {
        string url = "https://api.postcodes.io/postcodes/" + postCode + "/autocomplete";        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(await result.Content.ReadAsStringAsync());
        List<PostCode> searchResult = new List<PostCode>();
        if(jsonData["result"] != null)
        {
            //Make bulk lookup
            url = "https://api.postcodes.io/postcodes";
            var payload = "{\"postcodes\":"+ jsonData["result"].ToString() + "}";
            HttpContent content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(url,content);
            var bulkJson = JsonSerializer.Deserialize<Dictionary<string, object>>(await resp.Content.ReadAsStringAsync());
            if(bulkJson.ContainsKey("result"))
            {
                System.Console.WriteLine("-----------PARSING BULK UPLOADS------");
                //System.Console.WriteLine(bulkJson["result"]);
                var finalResult = JsonSerializer.Deserialize<List<BulkPostCode>>(bulkJson["result"].ToString());
                finalResult.ForEach(x=>{
                    searchResult.Add(x.Result);
                });
                //searchResult = resp.Select(x=>x.Result).ToList();
                // System.Console.WriteLine(JsonSerializer.Serialize(finalRes));
                // System.Console.WriteLine("TOTAL RECORDS " + finalRes.Count);
                
            }
        }        
        return searchResult;
    }    
}