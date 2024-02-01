using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using MVC_Watch_UI.ViewModels;

namespace MVC_Watch_UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class StockController : Controller
	{
		private readonly StockService _stockService;
		private readonly ProductService _productService;
		private readonly IUnitOfWork _unitOfWork;
		public StockController(StockService stockService, ProductService productService, IUnitOfWork unitOfWork)
		{
			_stockService = stockService;
			_productService = productService;
			_unitOfWork = unitOfWork;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var stocks = await _stockService.GetAllStocksAsync(includeProperties: "Product");
			return View(stocks);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var products = await _productService.GetAllProductsAsync();
			AddStockViewModel stockViewModel = new AddStockViewModel()
			{
				Stock = new(),
				ProductList = products.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.ProductID.ToString()
				})
			};
			return View(stockViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AddStockViewModel stockViewModel)
		{
			if (ModelState.IsValid)
			{
				_stockService.AddStock(stockViewModel.Stock);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}
		#region API Call 
		[HttpGet]
		public async Task<IActionResult> GetAllStocks()
		{
			var stocks = await _stockService.GetAllStocksAsync(includeProperties: "Product");
			return Json(new { data = stocks });
		}
		#endregion
	}
}
