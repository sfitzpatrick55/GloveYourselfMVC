using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Task
{
    public class TaskDetail
    {
        public int TaskId { get; set; }

        [Display(Name = "Task")]
        public string TaskName { get; set; }

        [Display(Name= "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

