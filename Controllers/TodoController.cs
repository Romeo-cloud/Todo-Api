// Import necessary namespaces for API development
using Microsoft.AspNetCore.Mvc;
using TodoAPi.Contract;
using TodoAPi.Interface;
using TodoAPi.Models;

// Define the namespace for the controller
namespace TodoAPi.Controllers
{
    // Mark this class as an API controller
    [ApiController]
    // Define the route for this controller, which will be "api/todo"
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        // Declare a private variable to store the ITodoServices dependency
        private ITodoServices _todoServices;

        // Constructor to inject the ITodoServices dependency
        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices; // Assign the injected service to the private variable
        }

        // CREATE A NEW TODO ITEM
        [HttpPost] // Defines a POST request
        public async Task<IActionResult> CreateTodoItemAsync(CreateTodoRequest createTodoRequest)
        {
            // Check if the request model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if validation fails
            }

            try
            {
                // Call the service to create a new Todo item
                await _todoServices.CreateTodoItemAsync(createTodoRequest);
                return Ok(new { message = "Todo item created successfully" }); // Return success response
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if an exception occurs
                return StatusCode(500, new { message = "An error occurred while creating todo item", error = ex.Message });
            }
        }

        // GET ALL TODO ITEMS
        [HttpGet] // Defines a GET request
        public async Task<IActionResult> GetAllTodoItemsAsync()
        {
            try
            {
                // Fetch all Todo items from the service
                var todoItems = await _todoServices.GetAllTodoItemsAsync();

                // If no items exist, return an empty list
                if (todoItems == null || !todoItems.Any())
                {
                    return Ok(new List<Todoitems>());
                }

                return Ok(todoItems); // Return the list of Todo items
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if an exception occurs
                return StatusCode(500, new { message = "An error occurred while getting todo items", error = ex.Message });
            }
        }

        // GET TODO ITEM BY ID
        [HttpGet("{id:guid}")] // Defines a GET request that expects a GUID parameter
        public async Task<IActionResult> GetTodoItemByIdAsync(Guid id)
        {
            try
            {
                // Fetch the Todo item by ID from the service
                var todoItem = await _todoServices.GetTodoItemByIdAsync(id);

                // If the item is not found, return 404 Not Found
                if (todoItem == null)
                {
                    return NotFound(new { message = "Todo not found" });
                }

                return Ok(todoItem); // Return the found Todo item
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if an exception occurs
                return StatusCode(500, new { message = "An error occurred while getting todo item", error = ex.Message });
            }
        }

        // UPDATE TODO ITEM
        [HttpPut("{id:guid}")] // Defines a PUT request that expects a GUID parameter
        public async Task<IActionResult> UpdateTodoItemAsync(Guid id, UpdateToDoRequest updateToDoRequest)
        {
            // Check if the request model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if validation fails
            }
            try
            {
                // Fetch the Todo item by ID to ensure it exists
                var todoItem = await _todoServices.GetTodoItemByIdAsync(id);
                if (todoItem == null)
                {
                    return NotFound(new { message = "Todo not found" }); // Return 404 if item doesn't exist
                }

                // Call the service to update the Todo item
                await _todoServices.UpdateTodoItemAsync(id, updateToDoRequest);
                return Ok(new { message = "Todo item updated successfully" }); // Return success response
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if an exception occurs
                return StatusCode(500, new { message = "An error occurred while updating todo item", error = ex.Message });
            }
        }

        // DELETE TODO ITEM
        [HttpDelete("{id}")] // Defines a DELETE request that expects an ID parameter
        public async Task<IActionResult> DeleteTodoItemAsync(Guid id)
        {
            try
            {
                // Call the service to delete the Todo item
                await _todoServices.DeleteTodoItemAsync(id);
                return Ok(new { message = "Todo item deleted successfully" }); // Return success response
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error if an exception occurs
                return StatusCode(500, new { message = "An error occurred while deleting todo item", error = ex.Message });
            }
        }
    }
}
