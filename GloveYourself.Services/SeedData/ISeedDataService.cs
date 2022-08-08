
namespace GloveYourself.Services.SeedData
{
    public interface ISeedDataService
    {
        Task<bool> SeedCategories();
    }
}