using System.Diagnostics;
using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ToDoList_Mvc.Models;

namespace ToDoList_Mvc.Controllers
{
 public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskServices _taskService;

        public HomeController(ILogger<HomeController> logger, TaskServices taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        private Guid? GetUserId()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return null;

            return Guid.Parse(userIdString);
        }

        public async Task<IActionResult> Index(string search)
        {

            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var all = await _taskService.GetAllAsync(userId.Value);

            List<TaskViewModel> filtered = all;

            if (!string.IsNullOrWhiteSpace(search))
            {
                filtered = all
                    .Where(t => t.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var vm = new TasksListViewModel
            {
                Tasks = all,
                Filtered = filtered,
                SearchTerm = search
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "No se debe dejar el campo vacío.";
                return RedirectToAction("Index");
            }

            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            await _taskService.AddTaskAsync(name, userId.Value);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "No se debe dejar el campo vacío.";
                return RedirectToAction("GetTaskUpdate", new { id = id });
            }

            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            await _taskService.EditTaskAsync(userId.Value, id, name);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetTaskUpdate(Guid id)
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var task = (await _taskService.GetAllAsync(userId.Value))
                .FirstOrDefault(x => x.Id == id);

            if (task == null)
                return RedirectToAction("Index");

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            await _taskService.DeleteTaskAsync(userId.Value, id);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }

}
