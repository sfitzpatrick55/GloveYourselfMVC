using GloveYourself.Data.Data;
using GloveYourself.Data.Models;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Models.Task;
using GloveYourself.Services.Category;
using GloveYourself.Services.Task;
using Microsoft.EntityFrameworkCore;

namespace GloveYourself.Services.Glove
{
    public class GloveService : IGloveService
    {
        private Guid _userId;

        private readonly ApplicationDbContext _context;

        public GloveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateGlove(GloveCreate model)
        {
            var entity = new GloveEntity()
            {
                OwnerId = _userId,
                Title = model.Title,
                Brand = model.Brand,
                Description = model.Description,
                CategoryId = model.CategoryId,
                TaskId = model.TaskId,
                CreatedUtc = DateTimeOffset.Now
            };

            _context.Gloves.Add(entity);

            return _context.SaveChanges() == 1;
        }

        public IEnumerable<GloveListItem> GetGloves()
        {
            var query = _context.Gloves.Where(g => g.OwnerId == _userId)
                .OrderBy(g => g.CategoryId)
                .Select(g => new GloveListItem
                {
                    GloveId = g.GloveId,
                    Title = g.Title,
                    Brand = g.Brand,
                    Description = g.Description,
                    CreatedUtc = g.CreatedUtc,
                    Category = g.CategoryEntity.CategoryName,
                    UserTask = g.TaskEntity.TaskName
                }
            );

            return query.ToArray();
        }

        public GloveDetail GetGloveById(int id)
        {
            var entity = _context.Gloves
                .OrderBy(g => g.CategoryId)
                .Include(c => c.CategoryEntity)
                .Include(t => t.TaskEntity)
                .Single(g => g.GloveId == id && g.OwnerId == _userId);

            return new GloveDetail
            {
                GloveId = entity.GloveId,
                Title = entity.Title,
                Brand = entity.Brand,
                Description = entity.Description,
                CreatedUtc = entity.CreatedUtc,
                Category = entity.CategoryEntity.CategoryName,
                UserTask = entity.TaskEntity.TaskName
            };
        }

        public bool UpdateGlove(GloveEdit model)
        {
            var entity = _context.Gloves.FirstOrDefault(g => g.GloveId == model.GloveId && g.OwnerId == _userId);

            entity.Title = model.Title;
            entity.Brand = model.Brand;
            entity.Description = model.Description;
            entity.CategoryId = model.CategoryId;
            entity.TaskId = model.TaskId;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteGlove(int gloveId)
        {
            var entity = _context.Gloves.FirstOrDefault(g => g.GloveId == gloveId && g.OwnerId == _userId);

            _context.Gloves.Remove(entity);

            return _context.SaveChanges() == 1;
        }


        //
        // Helpers

        public void SetUserId(Guid userId) => _userId = userId;


        public IEnumerable<CategoryListItem> CreateCategoryDropDownList()
        {
            var categoryService = new CategoryService(_context);

            categoryService.SetUserId(_userId);

            var categories = categoryService.GetCategories();

            return categories;
        }

        public IEnumerable<TaskListItem> CreateTaskDropDownList()
        {
            var taskService = new TaskService(_context);

            taskService.SetUserId(_userId);

            var tasks = taskService.GetTasks();

            return tasks;
        }
    }
}

