using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace http11fa
{
    public class http1
    {
        private readonly ILogger<http1> _logger;

        public http1(ILogger<http1> logger)
        {
            _logger = logger;
        }

        [Function("http1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            try
            {
                await Task.Delay(10000);

                _logger.LogInformation("Processing request...");
                // Function logic here
            }
            catch (IOException ex) when (ex.Message.Contains("transport connection"))
            {
                _logger.LogWarning("Client disconnected prematurely.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                //throw;
            }

            return new OkObjectResult("Welcome to Azure Functions!");

        }
    }
}
