using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostCodeSvc
{

    public class Function
    {
        private ServiceCollection _serviceCollection;
        private PostCodeService _postCodeService;
        private IMapper _mapper;

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Function()
        {
            ConfigureServices();
            var serviceProvider = _serviceCollection.BuildServiceProvider();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _postCodeService = serviceProvider.GetRequiredService<PostCodeService>();
        }

        /// <summary>
        /// Handler function of the lambda 
        /// </summary>
        /// <param name="apigProxyEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            string postCode = apigProxyEvent.QueryStringParameters["postcode"];
            context.Logger.Log("api invoked " + postCode);
            List<PostCodeDto> body = new List<PostCodeDto>();

            switch (apigProxyEvent.Path)
            {
                case AppSettings.PostCodeApiLookup:
                    var resp = _mapper.Map<PostCodeDto>(await _postCodeService.Lookup(postCode));
                    if (resp.Country != null)
                        body.Add(resp);
                    break;
                case AppSettings.PostCodeApiAutoCompletePath:
                    body = _mapper.Map<List<PostCodeDto>>(await _postCodeService.AutoComplete(postCode));
                    break;
                default:
                    break;
            }
            return new APIGatewayProxyResponse
            {
                Body = JsonSerializer.Serialize(body),
                StatusCode = body.Count > 0 ? 200 : 404,
                Headers = new Dictionary<string, string> { 
                    { "Content-Type", "application/json" },
                    {"Access-Control-Allow-Origin", "*" },
                    {"Access-Control-Allow-Headers", "*" } ,
                    {"Access-Control-Allow-Methods","*"}}
            };
        }


        #region Helpers
        private void ConfigureServices()
        {
            // add dependencies here
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddTransient<PostCodeService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            _serviceCollection.AddSingleton(mapper);
        }
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                // Add as many of these lines as you need to map your objects
                CreateMap<PostCode, PostCodeDto>();
            }
        }
        #endregion

    }
}
