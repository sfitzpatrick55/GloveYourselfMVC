using GloveYourself.Models.Glove;

namespace GloveYourself.Services.Glove
{
    public interface IGloveService
    {
        bool CreateGlove(GloveCreate model);

        IEnumerable<GloveListItem> GetGloves();
    }
}