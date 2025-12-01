using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList_Mvc.Controllers
{
    public class AccountController : Controller
    {
     
            private readonly UserServices _service;

            public AccountController(UserServices service)
            {
                _service = service;
            }

            public IActionResult Login() => View();

            [HttpPost]
            public async Task<IActionResult> Login(string username, string password)
            {
                var user = await _service.AuthenticateAsync(username, password);
                if (user == null)
                {
                    TempData["Error"] = "Usuario o contraseña incorrectos";
                    return View();
                }

        
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }

            public IActionResult Register() => View();

            [HttpPost]
            public async Task<IActionResult> Register(string username, string password)
            {
                try
                {
                    var user = await _service.RegisterAsync(username, password);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return View();
                }
            }

            public IActionResult Logout()
            {
                HttpContext.Session.Remove("UserId");
                return RedirectToAction("Login");
            }
        }

    }
