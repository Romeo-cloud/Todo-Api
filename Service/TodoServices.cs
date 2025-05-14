using AutoMapper;
using TodoAPi.Context;
using TodoAPi.Contract;
using TodoAPi.Interface;
using TodoAPi.Models;

namespace TodoAPi.Service
{
    // This class implements ITodoServices and contains business logic for handling To-Do items.
    public class TodoServices : ITodoServices
    {
        // Dependency injection for database context, AutoMapper, and logging
        private readonly ToDODbcontext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TodoServices> _logger;

        // Constructor for injecting dependencies
        public TodoServices(ToDODbcontext context, IMapper mapper, ILogger<TodoServices> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // Method to create a new To-Do item
        public async Task<Todoitems> CreateTodoItemAsync(CreateTodoRequest createTodoRequest)
        {
            try
            {
                // Map the request DTO to the Todoitems entity
                var todoItem = _mapper.Map<Todoitems>(createTodoRequest);
                todoItem.CreatedAt = DateTime.UtcNow; // Set creation timestamp

                _context.Todoitems.Add(todoItem); // Add new item to database
                await _context.SaveChangesAsync(); // Save changes asynchronously

                return todoItem; // Return the created item
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a todo item");
                throw new Exception("An error occurred while creating todo item");
            }
        }

        // Method to retrieve all To-Do items
        public Task<IEnumerable<Todoitems>> GetAllTodoItemsAsync()
        {
            var todoItems = _context.Todoitems.ToList(); // Fetch all items from the database

            if (todoItems.Count == 0)
            {
                throw new Exception("No todo items found"); // Throw an exception if no items exist
            }

            return Task.FromResult(todoItems.AsEnumerable()); // Return the list of items
        }

        // Method to retrieve a specific To-Do item by its ID
        public Task<Todoitems> GetTodoItemByIdAsync(Guid id)
        {
            var todoItem = _context.Todoitems.FirstOrDefault(x => x.Id == id); // Find the item by ID

            if (todoItem == null)
            {
                throw new Exception("Todo item not found"); // Throw exception if item does not exist
            }

            return Task.FromResult(todoItem); // Return the found item
        }

        // Method to update an existing To-Do item
        public async Task<Todoitems> UpdateTodoItemAsync(Guid id, UpdateToDoRequest updateTodoRequest)
        {
            try
            {
                var todoItem = await _context.Todoitems.FindAsync(id); // Find the item in the database

                if (todoItem == null)
                {
                    throw new Exception("Todo item not found");
                }

                // Update only the fields that have new values
                if (updateTodoRequest.Title != null)
                {
                    todoItem.Title = updateTodoRequest.Title;
                }
                if (updateTodoRequest.Description != null)
                {
                    todoItem.Description = updateTodoRequest.Description;
                }
                if (updateTodoRequest.IsComplete != null)
                {
                    todoItem.IsComplete = updateTodoRequest.IsComplete.Value;
                }
                if (updateTodoRequest.DueDate != null)
                {
                    todoItem.DueDate = updateTodoRequest.DueDate.Value;
                }
                if (updateTodoRequest.priority != null)
                {
                    todoItem.priority = updateTodoRequest.priority.Value;
                }

                await _context.SaveChangesAsync(); // Save changes asynchronously

                return todoItem; // Return the updated item
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a todo item");
                throw new Exception("An error occurred while updating todo item");
            }
        }

        // Method to delete a To-Do item by ID
        public Task<Todoitems> DeleteTodoItemAsync(Guid id)
        {
            try
            {
                var todoItem = _context.Todoitems.FirstOrDefault(x => x.Id == id); // Find the item

                if (todoItem == null)
                {
                    throw new Exception($"Todo item with this {id} is not found"); // If not found, throw an exception
                }

                _context.Todoitems.Remove(todoItem); // Remove the item from the database
                _context.SaveChanges(); // Save changes

                return Task.FromResult(todoItem); // Return the deleted item
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a todo item");
                throw new Exception("An error occurred while deleting todo item");
            }
        }
    }
}
