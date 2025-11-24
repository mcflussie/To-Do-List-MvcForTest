using System.Diagnostics;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using ToDoList_Mvc.Models;

namespace ToDoList_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        { 
            return View(TaskServices.model); 
        }

        public IActionResult AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "No se debe dejar el campo vacío.";
                return RedirectToAction("Index");
            }

            TaskServices.AddTask(name);

            return RedirectToAction("Index");
        }

        public IActionResult EditTask(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "No se debe dejar el campo vacío.";
                return RedirectToAction("GetTaskUpdate", new { id = id });
            }

            TaskServices.EditTask(id, name);

            return RedirectToAction("Index");
        }

        public IActionResult GetTaskUpdate(Guid id)
        {
            var task = TaskServices.model.Tasks.FirstOrDefault(x => x.Id == id);
            return View(task);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
