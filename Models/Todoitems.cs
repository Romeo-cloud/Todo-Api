// The Models folder contains classes that represent the structure of database tables.
// This class defines the schema for the "Todoitems" table.
namespace TodoAPi.Models
{
    public class Todoitems
    {
        // Unique identifier for each To-Do item (Primary Key in the database)
        public Guid Id { get; set; }

        // Title of the To-Do item (Short description)
        public string Title { get; set; }

        // Detailed description of the task
        public string Description { get; set; }

        // The deadline or due date for the task
        public DateTime DueDate { get; set; }

        // Boolean flag indicating whether the task is completed or not
        public bool IsComplete { get; set; }

        // Priority level of the task (e.g., 1 - Low, 5 - High)
        public int priority { get; set; }

        // Timestamp indicating when the task was created
        public DateTime CreatedAt { get; set; }

        // Timestamp indicating the last time the task was updated
        public DateTime UpdatedAt { get; set; }

        // Constructor to initialize default values
        public Todoitems()
        {
            IsComplete = false; // By default, a new task is not completed
        }
    }
}
