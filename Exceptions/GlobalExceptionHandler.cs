// Importing necessary namespaces for handling HTTP responses, logging, and error handling
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TodoAPi.Contract;

namespace TodoAPi.Exceptions
{
    // This class provides global exception handling in the API
    public class GlobalExceptionHandler : IExceptionHandler
    {
        // Private logger instance for logging errors
        private readonly ILogger<GlobalExceptionHandler> _logger;

        // Constructor that initializes the logger
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        // Method to handle exceptions asynchronously
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            // Logs the exception details using the injected logger
            _logger.LogError(exception, exception.Message);

            // Creating an error response object to structure error messages
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message // Assigning the exception message to the response
            };

            // Handling specific types of exceptions with different status codes
            switch (exception)
            {
                case BadHttpRequestException:
                    // If the exception is a bad HTTP request, set status code to 400 (Bad Request)
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name; // Setting the error title
                    break;

                default:
                    // For all other exceptions, return 500 (Internal Server Error)
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "An error occurred"; // Generic error title
                    break;
            }

            // Setting the HTTP response status code
            httpContext.Response.StatusCode = errorResponse.StatusCode;

            // Returning the error response as a JSON object
            await httpContext.Response.WriteAsJsonAsync(errorResponse);

            // Indicating that the exception has been handled
            return true;
        }
    }
}
