using GloveYourself.Models.Category;

namespace GloveYourself.Services.Category
{
    public interface ICategoryService
    {
        bool CreateCategory(CategoryCreate model);

        IEnumerable<CategoryListItem> GetCategories();

        CategoryDetail GetCategoryById(int id);

        bool UpdateCategory(CategoryEdit model);

        bool DeleteCategory(int categoryId);

        void SetUserId(Guid userId);
    }
}