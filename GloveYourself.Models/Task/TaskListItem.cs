using System.ComponentModel.DataAnnotations;

namespace GloveYourself.Models.Task
{
    public class TaskListItem
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}

