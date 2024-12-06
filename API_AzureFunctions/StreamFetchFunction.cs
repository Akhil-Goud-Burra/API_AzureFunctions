using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace API_AzureFunctions
{
    public class StreamFetchFunction
    {
        private readonly ILogger _logger;

        public StreamFetchFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<StreamFetchFunction>();
        }

        [Function("StreamFetchFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
