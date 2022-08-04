using GloveYourself.Models.Glove;

namespace GloveYourself.Services.Glove
{
    public interface IGloveService
    {
        bool CreateGlove(GloveCreate model);

        IEnumerable<GloveListItem> GetGloves();

        GloveDetail GetGloveById(int id);

        bool UpdateGlove(GloveEdit model);

        void SetUserId(Guid userId);
    }
}