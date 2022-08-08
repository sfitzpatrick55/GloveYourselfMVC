using GloveYourself.Models.Category;
using GloveYourself.Services.Category;
using GloveYourself.Services.SeedData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISeedDataService _seedDataService;

        public CategoryController(ICategoryService categoryService, ISeedDataService seedDataService)
        {
            _categoryService = categoryService;
            _seedDataService = seedDataService;
        }

        //
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            _seedDataService.SeedCategories(); // Calls method to check if Categories table in Db is empty, if so, seed data

            var model = _categoryService.GetCategories();

            return View(model);
        }

        //
        // GET: /category/create
        public IActionResult Create()
        {
            return View();
        }

        //
        // POST: /category/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (_categoryService.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry, your category could not be created.");

            return View(model);
        }

        //
        // GET: /category/details/{id}
        public IActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _seedDataService.SeedCategories(); // Calls method to check if Categories table in Db is empty, if so, seed data

            var model = _categoryService.GetCategoryById(id);

            return View(model);
        }

        //
        // GET: /category/edit/{id}
        public IActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var detail = _categoryService.GetCategoryById(id);

            var model = new CategoryEdit
            {
                CategoryId = detail.CategoryId,
                CategoryName = detail.CategoryName,
                Description = detail.Description
            };

            return View(model);
        }

        //
        // POST: /category/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch!");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _categoryService.GetCategoryById(id);

            if (_categoryService.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was successfully updated.";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sorry, your category could not be updated.");

            return View(model);
        }

        //
        // GET: /category/delete/{id}
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _categoryService.GetCategoryById(id);

            return View(model);
        }

        //
        // POST /category/delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _categoryService.DeleteCategory(id);

            TempData["SaveResult"] = "Your category was successfully deleted.";

            return RedirectToAction("Index");
        }

        //
        // Helpers

        private Guid GetUserId()
        {
            var userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null) return default;

            return Guid.Parse(userIdClaim);
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            //if everything works from above...
            _categoryService.SetUserId(userId);
            return true;
        }
    }
}
