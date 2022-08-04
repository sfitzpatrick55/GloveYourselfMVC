using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloveYourself.Models.Glove;
using GloveYourself.Services.Glove;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class GloveController : Controller
    {
        //
        // GET: /<controller>/
        public IActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GloveService(userId);
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
            if (ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GloveService(userId);

            service.CreateGlove(model);

            return RedirectToAction("Index");
        }
    }
}

