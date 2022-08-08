using GloveYourself.Models.Task;

namespace GloveYourself.Services.Task
{
    public interface ITaskService
    {
        bool CreateTask(TaskCreate model);

        IEnumerable<TaskListItem> GetTasks();

        TaskDetail GetTaskById(int id);

        bool UpdateTask(TaskEdit model);

        bool DeleteTask(int taskId);

        void SetUserId(Guid userId);
    }
}