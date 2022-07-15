using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostCodeSvc
{

    public class Function
    {
     public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            string postCode = apigProxyEvent.QueryStringParameters["postcode"];
            
            List<PostCode> body = new List<PostCode>();
            
            switch (apigProxyEvent.Path)
            {
                case "/lookup":
                    var resp = await new PostCodeService().PostCodeLookup(postCode); 
                    if(resp.Country != null)
                        body.Add(resp);
                    break;
                case "/autocomplete":
                    body = await new PostCodeService().AutoComplete(postCode);
                break;
                default:
                break;
            }
        //Mapping the final result to DTO 
        //Alternatively we can use auto mapper 
        List<PostCodeModel> results = new List<PostCodeModel>(); 
        body.ForEach(x=> results.Add(new PostCodeModel() { 
                    PostCode = x.Code,
                    Country = x.Country,
                    Region = x.Region,
                    AdminDistrict = x.AdminDistrict,
                    ParliamentaryConstituency = x.ParliamentaryConstituency,
                    Area = x.Area
                }));
            return new APIGatewayProxyResponse
            {
                Body = JsonSerializer.Serialize(results),
                StatusCode = body.Count > 0 ? 200 : 404,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" },{"Access-Control-Allow-Origin", "*" } }
            };
        }   
    }
}
