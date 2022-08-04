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
            var service = CreateGloveService();

            var model = service.GetGloves();

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
            if (ModelState.IsValid) return View(model);

            var service = CreateGloveService();

            if (service.CreateGlove(model))
            {
                TempData["SaveResult"] = "Your glove was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry, your glove could not be created.");

            return View(model);
        }

        //
        // GET: /glove/details
        public IActionResult Details(int id)
        {
            var service = CreateGloveService();
            var model = service.GetGloveById(id);

            return View(model);
        }

        private GloveService CreateGloveService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GloveService(userId);
            return service;

            //ClaimsPrincipal currentUser = this.User;

            //var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            //var service = new GloveService(currentUserId, _context);

            //return service;
        }
    }
}

