using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiAuth
{
    public class Function
    {
     public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest  request, ILambdaContext context)
        {
            System.Console.WriteLine("API INVOKED");
            var effect = "Deny";
            if(request.AuthorizationToken == "12345" )
            {
                effect = "Allow";
            }
            return new APIGatewayCustomAuthorizerResponse {
                PolicyDocument = new APIGatewayCustomAuthorizerPolicy
                {
                    Statement = new List<Amazon.Lambda.APIGatewayEvents.APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>
                    {
                        new Amazon.Lambda.APIGatewayEvents.APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement(){
                            Action = new HashSet<string>{ "execute-api:Invoke" },
                            Effect = effect,
                            Resource = new HashSet<string>{ request.MethodArn }
                        }
                    }
                }
            };
        }   
    }
}