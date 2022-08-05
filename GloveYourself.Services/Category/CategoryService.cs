using GloveYourself.Data.Data;
using GloveYourself.Data.Models;
using GloveYourself.Models.Category;

namespace GloveYourself.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private Guid _userId;

        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new CategoryEntity()
            {
                OwnerId = _userId,
                CategoryName = model.CategoryName,
                Description = model.Description,
                CreatedUtc = DateTimeOffset.Now
            };

            _context.Categories.Add(entity);

            return _context.SaveChanges() == 1;
        }

        public IEnumerable<CategoryListItem> GetCategories()
        {
            var query = _context.Categories.Where(c => c.OwnerId == _userId)
                .Select(c => new CategoryListItem
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    CreatedUtc = c.CreatedUtc
                }
            );

            return query.ToArray();
        }

        public CategoryDetail GetCategoryById(int id)
        {
            var entity = _context.Categories.Single(c => c.CategoryId == id && c.OwnerId == _userId);

            return new CategoryDetail
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                CreatedUtc = entity.CreatedUtc
            };
        }

        public bool UpdateCategory(CategoryEdit model)
        {
            var entity = _context.Categories.FirstOrDefault(c => c.CategoryId == model.CategoryId && c.OwnerId == _userId);

            entity.CategoryName = model.CategoryName;
            entity.Description = model.Description;

            return _context.SaveChanges() == 1;
        }

        public bool DeleteCategory(int categoryId)
        {
            var entity = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId && c.OwnerId == _userId);

            _context.Categories.Remove(entity);

            return _context.SaveChanges() == 1;
        }


        //
        // Helpers

        public void SetUserId(Guid userId) => _userId = userId;
    }
}

