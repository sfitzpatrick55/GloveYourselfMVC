using GloveYourself.Models.Task;
using GloveYourself.Services.SeedData;
using GloveYourself.Services.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ISeedDataService _seedDataService; 

        public TaskController(ITaskService taskService, ISeedDataService seedDataService)
        {
            _taskService = taskService;
            _seedDataService = seedDataService;
        }

        //
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (!SetUserIdInService()) return Unauthorized();

            _seedDataService.SeedTasks(); // Calls method to check if Tasks table in Db is empty, if so, seed data

            var model = _taskService.GetTasks();

            return View(model);
        }

        //
        // GET: /task/create
        public IActionResult Create()
        {
            return View();
        }

        //
        // POST: /task/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskCreate model)
        {
            if (!SetUserIdInService()) return Unauthorized();

            if (_taskService.CreateTask(model))
            {
                TempData["SaveResult"] = "Your task was successfully created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Sorry, your task could not be created.");

            return View(model);
        }

        //
        // GET: /task/details/{id}
        public IActionResult Details(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _seedDataService.SeedTasks(); // Calls method to check if Tasks table in Db is empty, if so, seed data

            var model = _taskService.GetTaskById(id);

            return View(model);
        }

        //
        // GET: /task/edit/{id}
        public IActionResult Edit(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var detail = _taskService.GetTaskById(id);

            var model = new TaskEdit
            {
                TaskId = detail.TaskId,
                TaskName = detail.TaskName,
            };

            return View(model);
        }

        //
        // POST: /task/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TaskId != id)
            {
                ModelState.AddModelError("", "Id Mismatch!");
                return View(model);
            }

            if (!SetUserIdInService()) return Unauthorized();

            var detail = _taskService.GetTaskById(id);

            if (_taskService.UpdateTask(model))
            {
                TempData["SaveResult"] = "Your task was successfully updated.";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sorry, your task could not be updated.");

            return View(model);
        }

        //
        // GET: /task/delete/{id}
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var model = _taskService.GetTaskById(id);

            return View(model);
        }

        //
        // POST /task/delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            _taskService.DeleteTask(id);

            TempData["SaveResult"] = "Your task was successfully deleted.";

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
            _taskService.SetUserId(userId);
            return true;
        }
    }
}
