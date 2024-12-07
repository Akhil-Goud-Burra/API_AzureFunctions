using System.Net;
using System.Text.Json;
using API_AzureFunctions.ErrorHandling.CustomExceptionResponse;
using API_AzureFunctions.Models;
using API_AzureFunctions.RepositoryPattern.IRepository;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace API_AzureFunctions
{
    public class StreamFetchFunction
    {
        private readonly ILogger _logger;

        private IRepositoryStream _repository;

        public StreamFetchFunction(ILoggerFactory loggerFactory, IRepositoryStream repository)
        {
            _logger = loggerFactory.CreateLogger<StreamFetchFunction>();
            _repository = repository;
        }

        [Function("StreamFetchFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            try
            {
                // Fetching data from the database
                var results = _repository.GetAll_Stream();

                // Serializing the fetched data to JSON
                var jsonResponse = JsonSerializer.Serialize(results);

                // Writing JSON response
                response.WriteString(jsonResponse);
            }
            catch (ApiException apiEx)
            {
                _logger.LogError(apiEx, "Custom API exception occurred.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.WriteString(apiEx.Message);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.WriteString("Unhandled exception, An internal error occurred.");
            }

            return response;
        }
    }
}
