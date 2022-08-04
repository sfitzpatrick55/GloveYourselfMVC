using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloveYourself.Models.Glove;
using GloveYourself.Services.Glove;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using GloveYourself.Data.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class GloveController : Controller
    {
        private readonly IGloveService _gloveService;

        public GloveController(IGloveService gloveService)
        {
            _gloveService = gloveService;
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
            return View();
        }

        //
        // POST: /glove/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GloveCreate model)
        {
            //if (ModelState.IsValid) return View(model);

            if (!SetUserIdInService()) return Unauthorized();

            //var service = _gloveService.GetGloves();
            //var service = CreateGloveService();

            if (_gloveService.CreateGlove(model))
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

            //var service = CreateGloveService();
            //var model = service.GetGloveById(id);

            return View(model);
        }

        //
        // GET: /glove/edit/{id}
        public IActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var detail = _gloveService.GetGloveById(id);

            //var service = CreateGloveService();
            //var detail = service.GetGloveById(id);

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

            ModelState.AddModelError("", "Sorry, your note could not be updated.");

            return View(model);
        }

        //
        // GET: /glove/delete/{id}
        public IActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _gloveService.GetGloveById(id);

            //var service = CreateGloveService();
            //var model = service.GetGloveById(id);

            return View(model);
        }

        //
        // POST /glove/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            //var model = _gloveService.GetGloveById(id);

            _gloveService.DeleteGlove(id);

            TempData["SaveResult"] = "Your note was successfully deleted.";

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
    }
}

