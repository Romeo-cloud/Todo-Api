// Defining the namespace for better project organization
namespace TodoAPi.Contract
{
    // Class representing an error response model
    public class ErrorResponse
    {
        // Title of the error (e.g., "Validation Error", "Server Error")
        public string Title { get; set; }

        // HTTP status code representing the error (e.g., 400 for Bad Request, 500 for Server Error)
        public int StatusCode { get; set; }

        // A detailed message describing the error
        public string Message { get; set; }
    }
}
