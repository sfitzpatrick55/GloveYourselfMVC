using GloveYourself.Models.Task;

namespace GloveYourself.Services.Task
{
    public interface ITaskService
    {
        bool CreateTask(TaskCreate model);

        bool DeleteTask(int taskId);

        TaskDetail GetTaskById(int id);

        IEnumerable<TaskListItem> GetTasks();

        void SetUserId(Guid userId);

        bool UpdateTask(TaskEdit model);
    }
}