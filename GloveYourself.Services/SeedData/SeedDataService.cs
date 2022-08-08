using GloveYourself.Data.Data;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Models.Task;
using GloveYourself.Services.Category;
using GloveYourself.Services.Glove;
using GloveYourself.Services.Task;

namespace GloveYourself.Services.SeedData
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ApplicationDbContext _context;
        //private readonly ICategoryService _categoryService;
        //private readonly IGloveService _gloveService;
        //private readonly ITaskService _taskService;

        public SeedDataService(ApplicationDbContext context)
        {
            _context = context;
            //_categoryService = categoryService;
            //_gloveService = gloveService;
            //_taskService = taskService;
        }

        public async Task<bool> SeedCategories()
        {
            var _categoryService = new CategoryService(_context);

            int items = _context.Categories.Count();
            if (items == 0)
            {
                var disposable = new CategoryCreate()
                {
                    CategoryName = "Disposable",
                    Description = "Gloves designed for single use only"
                };

                var leather = new CategoryCreate()
                {
                    CategoryName = "Leather",
                    Description = "Gloves made with over 50% leather material"
                };

                var cutResistant = new CategoryCreate()
                {
                    CategoryName = "Cut-Resistant",
                    Description = "ANSI / ISEA Cut Level A1 through A9"
                };

                var nitrileDipped = new CategoryCreate()
                {
                    CategoryName = "Nitrile Dipped",
                    Description = "Gloves dipped in liquid nitrile for better chemical resistance"
                };

                var allPurpose = new CategoryCreate()
                {
                    CategoryName = "All-Purpose",
                    Description = "Reusable gloves designed for general tasks"
                };

                var impactResistant = new CategoryCreate()
                {
                    CategoryName = "Impact-Resistant",
                    Description = "Gloves that incorporate molded rubber components to protect from impact and crushing"
                };

                var latexGrip = new CategoryCreate()
                {
                    CategoryName = "Latex Grip",
                    Description = "A collection of knitted gloves dipped in liquid latex to enhance grip"
                };

                _categoryService.CreateCategory(disposable);
                _categoryService.CreateCategory(leather);
                _categoryService.CreateCategory(cutResistant);
                _categoryService.CreateCategory(nitrileDipped);
                _categoryService.CreateCategory(allPurpose);
                _categoryService.CreateCategory(impactResistant);
                return _categoryService.CreateCategory(latexGrip);
            }
            else
            {
                return false;
            }
        }

        //public async Task<bool> SeedUserProfilesAsync()
        //{
        //    int userCount = _context.UserProfile.Count();
        //    if (userCount == 0)
        //    {
        //        var firstUser = new UserProfileRegister()
        //        {
        //            Username = "UserOne",
        //            Password = "UserOnePassword",
        //            ConfirmPassword = "UserOnePassword",
        //            CookingExperienceLevel = "Low",
        //            FirstName = "User",
        //            LastName = "One",
        //            FavoriteFood = "Escargot"
        //        };

        //        var secondUser = new UserProfileRegister()
        //        {
        //            Username = "UserTwo",
        //            Password = "UserTwoPassword",
        //            ConfirmPassword = "UserTwoPassword",
        //            CookingExperienceLevel = "High",
        //            FirstName = "User",
        //            LastName = "Two",
        //            FavoriteFood = "Onions"
        //        };

        //        var thirdUser = new UserProfileRegister()
        //        {
        //            Username = "UserThree",
        //            Password = "UserThreePassword",
        //            ConfirmPassword = "UserThreePassword",
        //            CookingExperienceLevel = "Medium",
        //            FirstName = "User",
        //            LastName = "Three",
        //            FavoriteFood = "Pizza with or without Pineapple"
        //        };

        //        await _userProfile.RegisterUserProfileAsync(firstUser);
        //        await _userProfile.RegisterUserProfileAsync(secondUser);
        //        await _userProfile.RegisterUserProfileAsync(thirdUser);
        //        return true;
        //    }

        //    return false;
        //}

        //public async Task<bool> SeedShoppingListAsync()
        //{
        //    int items = _context.ShoppingList.Count();
        //    if (items == 0)
        //    {
        //        var firstShoppingList = new ShoppingListCreate()
        //        {
        //            IngredientName = "Avocado",
        //            Amount = "4",
        //        };

        //        var secondShoppingList = new ShoppingListCreate()
        //        {
        //            IngredientName = "Butter",
        //        };

        //        var thirdShoppingList = new ShoppingListCreate()
        //        {
        //            IngredientName = "Kosher salt",
        //        };

        //        await _shoppingList.CreateShoppingListAsync(firstShoppingList);
        //        await _shoppingList.CreateShoppingListAsync(secondShoppingList);
        //        await _shoppingList.CreateShoppingListAsync(thirdShoppingList);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public async Task<bool> SeedFavoritedRecipesAsync()
        //{
        //    int items = _context.FavoritedRecipes.Count();
        //    if (items == 0)
        //    {
        //        var firstFavoritedRecipe = new FavoritedRecipesCreate()
        //        {
        //            UserId = 1005,
        //            RecipeId = 4,
        //        };

        //        var secondFavoritedRecipe = new FavoritedRecipesCreate()
        //        {
        //            UserId = 1006,
        //            RecipeId = 9,
        //        };

        //        var thirdFavoritedRecipe = new FavoritedRecipesCreate()
        //        {
        //            UserId = 1007,
        //            RecipeId = 12,
        //        };

        //        await _favoritedRecipe.CreateFavoritedRecipesAsync(firstFavoritedRecipe);
        //        await _favoritedRecipe.CreateFavoritedRecipesAsync(secondFavoritedRecipe);
        //        await _favoritedRecipe.CreateFavoritedRecipesAsync(thirdFavoritedRecipe);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}