using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using PostCodeSvc;
using System.Collections.Generic;

public class FunctionTest
{
    public FunctionTest()
    {
    }

    [Fact]
    public void TestLookup_Ok()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        Function function = new Function();

        request = new APIGatewayProxyRequest();
        context = new TestLambdaContext();
        request.HttpMethod = "GET";
        request.QueryStringParameters = new Dictionary<string, string>();
        request.QueryStringParameters["postcode"] = "ne31aa";
        request.Path = "/lookup";
        response = function.FunctionHandler(request, context).Result;
        Assert.Equal(200, response.StatusCode);
        Assert.True(response.Body != null);
        Assert.True(response.Body.Length > 0);
    }

    [Fact]
    public void TestLookup_InvalidPostCode()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        Function function = new Function();

        request = new APIGatewayProxyRequest();
        context = new TestLambdaContext();
        request.HttpMethod = "GET";
        request.QueryStringParameters = new Dictionary<string, string>();
        request.QueryStringParameters["postcode"] = "abcdef";
        request.Path = "/lookup";
        
        Assert.Throws<System.AggregateException>(() => function.FunctionHandler(request, context).Result);
    }

    [Fact]
    public void TestAutocomplete_Ok()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        Function function = new Function();

        request = new APIGatewayProxyRequest();
        context = new TestLambdaContext();
        request.HttpMethod = "GET";
        request.QueryStringParameters = new Dictionary<string, string>();
        request.QueryStringParameters["postcode"] = "Ne3";
        request.Path = "/autocomplete";
        response = function.FunctionHandler(request, context).Result;
        Assert.Equal(200, response.StatusCode);
        Assert.True(response.Body != null);
        Assert.True(response.Body.Length > 1);
    }

    [Fact]
    public void TestAutocomplete_InvalidPostCode()
    {
        TestLambdaContext context;
        APIGatewayProxyRequest request;
        APIGatewayProxyResponse response;

        Function function = new Function();

        request = new APIGatewayProxyRequest();
        context = new TestLambdaContext();
        request.HttpMethod = "GET";
        request.QueryStringParameters = new Dictionary<string, string>();
        request.QueryStringParameters["postcode"] = "abcdef";
        request.Path = "/autocomplete";
        response = function.FunctionHandler(request, context).Result;
        Assert.Equal(404, response.StatusCode);
    }
}