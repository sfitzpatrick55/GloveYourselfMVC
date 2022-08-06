using GloveYourself.Data.Data;
using GloveYourself.Data.Models;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Services.Category;
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
                CreatedUtc = DateTimeOffset.Now
            };

            _context.Gloves.Add(entity);

            return _context.SaveChanges() == 1;
        }

        public IEnumerable<GloveListItem> GetGloves()
        {
            var query = _context.Gloves.Where(g => g.OwnerId == _userId)
                .Select(g => new GloveListItem
                {
                    GloveId = g.GloveId,
                    Title = g.Title,
                    Brand = g.Brand,
                    Description = g.Description,
                    //CategoryId = g.CategoryId,
                    CreatedUtc = g.CreatedUtc,
                    Category = g.CategoryEntity.CategoryName
                }
            );

            return query.ToArray();
        }

        public GloveDetail GetGloveById(int id)
        {
            var entity = _context.Gloves
                .Include(g => g.CategoryEntity)
                .Single(g => g.GloveId == id && g.OwnerId == _userId);

            return new GloveDetail
            {
                GloveId = entity.GloveId,
                Title = entity.Title,
                Brand = entity.Brand,
                Description = entity.Description,
                //CategoryId = entity.CategoryId,
                CreatedUtc = entity.CreatedUtc,
                //CategoryEntity = entity.CategoryEntity
                Category = entity.CategoryEntity.CategoryName
            };
        }

        public bool UpdateGlove(GloveEdit model)
        {
            var entity = _context.Gloves.FirstOrDefault(g => g.GloveId == model.GloveId && g.OwnerId == _userId);

            entity.Title = model.Title;
            entity.Brand = model.Brand;
            entity.Description = model.Description;
            entity.CategoryId = model.CategoryId;

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
    }
}

