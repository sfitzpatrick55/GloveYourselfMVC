using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Task
{
    public class TaskCreate
    {
        [Required]
        [Display(Name = "Task Name")]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "Sorry, there are too many characters in this field.")]
        public string TaskName { get; set; }
    }
}

