// we always use this to interact with users .... here is act like the form
// this will allow the user to create a todo...

using System.ComponentModel.DataAnnotations;

namespace TodoAPi.Contract
{
    public class CreateTodoRequest
    {
        [Required]
        [StringLength(60)] //: this make sure that the title is required and it cant exceed 60 words
        public string Title { get; set; }   
        public DateTime DueDate { get; set; }
        [StringLength(60)]
        public string Description { get; set; }
        [Range(1, 5)]//:this is to set range between 1 to 5 so the priority cant exceed 5
        public int priority { get; set; }
    }
}
