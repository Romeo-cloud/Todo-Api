// Importing necessary namespace for data validation attributes
using System.ComponentModel.DataAnnotations;

namespace TodoAPi.Contract
{
    // Class representing the request model for updating a To-Do item
    public class UpdateToDoRequest
    {
        // Title of the To-Do item, with a maximum length of 50 characters
        [StringLength(50)]
        public string Title { get; set; }

        // Description of the To-Do item, with a maximum length of 50 characters
        [StringLength(50)]
        public string Description { get; set; }

        // Nullable boolean indicating whether the task is completed
        public bool? IsComplete { get; set; }

        // Nullable DateTime indicating the due date of the task
        public DateTime? DueDate { get; set; }

        // Priority level of the task (1 to 5), ensuring values are within a valid range
        [Range(1, 5)]
        public int? priority { get; set; }
    }
}
