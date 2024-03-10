using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.AppUserDTO;
using MVC_Watch_Business.DTO.LoginDTO;
using MVC_Watch_Business.DTO.RegisterDTO;
using MVC_Watch_Business.Services;

namespace MVC_Watch_UI.Areas.Authentication.Controllers
{
    [Area("Authentication")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO req)
        {
            await _authService.Login(req);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDTO req)
        {
            await _authService.Register(req);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
