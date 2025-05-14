// Import necessary namespaces for using contracts and models
using TodoAPi.Contract;
using TodoAPi.Models;

namespace TodoAPi.Interface
{
    // Defining an interface for the Todo Services
    public interface ITodoServices
    {
        // Retrieves all Todo items from the database asynchronously
        Task<IEnumerable<Todoitems>> GetAllTodoItemsAsync();

        // Retrieves a single Todo item by its unique ID
        Task<Todoitems> GetTodoItemByIdAsync(Guid id);

        // Creates a new Todo item in the database
        Task<Todoitems> CreateTodoItemAsync(CreateTodoRequest todoItem);

        // Updates an existing Todo item using its unique ID
        Task<Todoitems> UpdateTodoItemAsync(Guid id, UpdateToDoRequest todoItem);

        // Deletes a Todo item from the database using its unique ID
        Task<Todoitems> DeleteTodoItemAsync(Guid id);
    }
}
