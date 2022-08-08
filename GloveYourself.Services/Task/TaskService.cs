using GloveYourself.Data.Data;
using GloveYourself.Data.Models;
using GloveYourself.Models.Task;

namespace GloveYourself.Services.Task
{
    public class TaskService : ITaskService
    {
        private Guid _userId;

        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateTask(TaskCreate model)
        {
            var entity = new TaskEntity()
            {
                OwnerId = _userId,
                TaskName = model.TaskName,
                CreatedUtc = DateTimeOffset.Now
            };

            _context.Tasks.Add(entity);

            return _context.SaveChanges() == 1;
        }

        public IEnumerable<TaskListItem> GetTasks()
        {
            var query = _context.Tasks.Where(t => t.OwnerId == _userId)
                .OrderBy(t => t.TaskName)
                .Select(t => new TaskListItem
                {
                    TaskId = t.TaskId,
                    TaskName = t.TaskName,
                    CreatedUtc = t.CreatedUtc
                }
            );

            return query.ToArray();
        }

        public TaskDetail GetTaskById(int id)
        {
            var entity = _context.Tasks
                .OrderBy(t => t.TaskName)
                .Single(t => t.TaskId == id && t.OwnerId == _userId);

            return new TaskDetail
            {
                TaskId = entity.TaskId,
                TaskName = entity.TaskName,
                CreatedUtc = entity.CreatedUtc
            };
        }

        public bool UpdateTask(TaskEdit model)
        {
            var entity = _context.Tasks.FirstOrDefault(t => t.TaskId == model.TaskId && t.OwnerId == _userId);

            entity.TaskName = model.TaskName;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteTask(int taskId)
        {
            var entity = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId && t.OwnerId == _userId);

            _context.Tasks.Remove(entity);

            return _context.SaveChanges() == 1;
        }


        //
        // Helpers

        public void SetUserId(Guid userId) => _userId = userId;
    }
}

