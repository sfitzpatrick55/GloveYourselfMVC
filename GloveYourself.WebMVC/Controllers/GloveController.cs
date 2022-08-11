using GloveYourself.Data.Models;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Models.Task;
using GloveYourself.Services.Glove;
using GloveYourself.Services.GloveSize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static GloveYourself.Data.Models.SizeEnum;

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class GloveController : Controller
    {
        private readonly IGloveService _gloveService;
        private readonly IGloveSizeService _gloveSizeService;

        public GloveController(IGloveService gloveService, IGloveSizeService gloveSizeService)
        {
            _gloveService = gloveService;
            _gloveSizeService = gloveSizeService;
        }

        //
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _gloveService.GetGloves();

            return View(model);
        }

        //
        // GET: /glove/create
        public IActionResult Create()
        {
            ViewBag.CategorySelectList = new SelectList(GetCategoryDropDownList(), "CategoryId", "CategoryName");

            ViewBag.TaskSelectList = new SelectList(GetTaskDropDownList(), "TaskId", "TaskName");

            return View();
        }

        //
        // POST: /glove/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GloveCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            ViewBag.CategorySelectList = new SelectList(GetCategoryDropDownList(), "CategoryId", "CategoryName");

            ViewBag.TaskSelectList = new SelectList(GetTaskDropDownList(), "TaskId", "TaskName");

            //if (_gloveService.CreateGlove(model))
            var newGlove = _gloveService.CreateGlove(model);

            _gloveSizeService.AddGloveSize(new { newGlove.GloveId, Size.Small, model.Smin, model.Smax });
            _gloveSizeService.AddGloveSize(new { newGlove.GloveId, Size.Medium, model.Mmin, model.Mmax });
            _gloveSizeService.AddGloveSize(new { newGlove.GloveId, Size.Large, model.Lmin, model.Lmax });
            _gloveSizeService.AddGloveSize(new { newGlove.GloveId, Size.XLarge, model.XLmin, model.XLmax });

            {
                TempData["SaveResult"] = "Your glove was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry, your glove could not be created.");

            return View(model);
        }

        //
        // GET: /glove/details/{id}
        public IActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _gloveService.GetGloveById(id);

            return View(model);
        }

        //
        // GET: /glove/edit/{id}
        public IActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            ViewBag.CategorySelectList = new SelectList(GetCategoryDropDownList(), "CategoryId", "CategoryName");

            ViewBag.TaskSelectList = new SelectList(GetTaskDropDownList(), "TaskId", "TaskName");

            var detail = _gloveService.GetGloveById(id);

            var model = new GloveEdit
            {
                GloveId = detail.GloveId,
                Title = detail.Title,
                Brand = detail.Brand,
                Description = detail.Description
            };

            return View(model);
        }

        //
        // POST: /glove/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GloveEdit model)
        {
            ViewBag.CategorySelectList = new SelectList(GetCategoryDropDownList(), "CategoryId", "CategoryName", model.CategoryId);

            ViewBag.TaskSelectList = new SelectList(GetTaskDropDownList(), "TaskId", "TaskName");

            if (!ModelState.IsValid) return View(model);

            if (model.GloveId != id)
            {
                ModelState.AddModelError("", "Id Mismatch!");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _gloveService.GetGloveById(id);

            if (_gloveService.UpdateGlove(model))
            {
                TempData["SaveResult"] = "Your glove was successfully updated.";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sorry, your glove could not be updated.");

            return View(model);
        }

        //
        // GET: /glove/delete/{id}
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _gloveService.GetGloveById(id);

            return View(model);
        }

        //
        // POST /glove/delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _gloveService.DeleteGlove(id);

            TempData["SaveResult"] = "Your glove was successfully deleted.";

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
            _gloveService.SetUserId(userId);
            return true;
        }

        private IEnumerable<CategoryListItem> GetCategoryDropDownList()
        {
            if (!SetUserIdInService()) return default;

            return _gloveService.CreateCategoryDropDownList();
        }

        private IEnumerable<TaskListItem> GetTaskDropDownList()
        {
            if (!SetUserIdInService()) return default;

            return _gloveService.CreateTaskDropDownList();
        }
    }
}
