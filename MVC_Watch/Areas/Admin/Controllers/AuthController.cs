using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.LoginDTO;
using MVC_Watch_Business.DTO.RegisterDTO;
using MVC_Watch_Business.Services;

namespace MVC_Watch_UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AuthController : Controller
	{
		private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
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
			HttpContext.Session.SetString("isAuthen", "OK");
			HttpContext.Session.SetString("UserName", $"{req.Username}");
			return RedirectToAction("Index", "Home", new {area = "Customer"});
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
			HttpContext.Session.SetString("isAuthen", "OK");
			return RedirectToAction("Index", "Home", new { area = "Customer" });
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _authService.Logout();
			HttpContext.Session.SetString("isAuthen", "NO");
			return RedirectToAction("Index", "Home", new { area = "Customer" });
		}

	}
}
