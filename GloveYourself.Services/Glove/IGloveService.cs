using GloveYourself.Data.Models;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Models.Task;

namespace GloveYourself.Services.Glove
{
    public interface IGloveService
    {
        GloveEntity CreateGlove(GloveCreate model);

        IEnumerable<GloveListItem> GetGloves();

        GloveDetail GetGloveById(int id);

        bool UpdateGlove(GloveEdit model);

        bool DeleteGlove(int gloveId);

        void SetUserId(Guid userId);

        IEnumerable<CategoryListItem> CreateCategoryDropDownList();

        IEnumerable<TaskListItem> CreateTaskDropDownList();
    }
}