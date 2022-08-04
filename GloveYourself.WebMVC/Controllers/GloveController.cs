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
            var service = CreateNoteService();

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
            
            var service = CreateNoteService();

            if (service.CreateGlove(model))
            {
                TempData["SaveResult"] = "Your glove was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry, your glove could not be created.");

            return View(model);
        }

        private GloveService CreateNoteService()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new GloveService(currentUserId, /* DbContext reference here */);

            return service;
        }
    }
}

