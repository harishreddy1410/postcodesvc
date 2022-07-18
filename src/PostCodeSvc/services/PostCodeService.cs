using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

/// <summary>
/// Service to make call's to postecodes.io API and parse the response 
/// </summary>
public class PostCodeService
{    
    private static readonly HttpClient client = new HttpClient();

    /// <summary>
    /// Postcode api for lookup 
    /// </summary>
    /// <param name="postCode"></param>
    /// <returns></returns>
    public async Task<PostCodeDetail> Lookup(string postCode)
    {
        string url = AppSettings.PostCodeApiUrl + postCode;        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>
        (await result.Content.ReadAsStringAsync());
        return jsonData.ContainsKey("result") ? JsonSerializer.Deserialize<PostCodeDetail>(jsonData["result"].ToString()) : new PostCodeDetail();   
    }

    /// <summary>
    /// Postcode api for autocomplete
    /// </summary>
    /// <param name="postCode"></param>
    /// <returns></returns>
    public async Task<List<PostCodeDetail>> AutoComplete(string postCode)
    {
        string url = AppSettings.PostCodeApiUrl + postCode + AppSettings.PostCodeApiAutoCompletePath;        
        var result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(await result.Content.ReadAsStringAsync());
        List<PostCodeDetail> searchResult = new List<PostCodeDetail>();
        if(jsonData["result"] != null)
        {
            //Make bulk lookup
            var payload = "{\"postcodes\":"+ jsonData["result"].ToString() + "}";
            HttpContent content = new StringContent(payload, System.Text.Encoding.UTF8, "application/json");
            var resp = await client.PostAsync(AppSettings.PostCodeApiUrl, content);
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