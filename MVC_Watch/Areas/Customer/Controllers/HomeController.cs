
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using MVC_Watch_Business.DTO.ProductDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_UI.Utils;
using System.Diagnostics;

namespace MVC_Watch_UI.Areas.Customer.Controllers
{
	[Area("Customer")]
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ProductService _productService;
		private readonly IMapper _mapper;
		public HomeController(ProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			await HttpContext.SignOutAsync();
			var productsDomain = await _productService.GetAllProductsAsync(includeProperties: "ProductDiscount");
			var products = _mapper.Map<IEnumerable<ProductDTO>>(productsDomain);
			return View(products);
		}

		[HttpGet]
		[ActionName("Shop")]
		public async Task<IActionResult> Shop(string? priceOrder, int? pageNumber, string? searchString)
		{
			ViewData["searchString"] = searchString;
			ViewData["priceOrder"] = string.IsNullOrEmpty(priceOrder) ? "" : priceOrder;
			ViewData["pageNumber"] = string.IsNullOrEmpty(searchString) ? "" : pageNumber;
			var productsDomain = await _productService.GetAllProductsAsync(includeProperties: "ProductDiscount");
			var products = _mapper.Map<IEnumerable<ProductDTO>>(productsDomain);

			// Handle searching
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(x => x.Name.ToLower().Contains(searchString.ToLower()));
			}
			// Handle sorting 
			if (!string.IsNullOrEmpty(priceOrder) && priceOrder == "LH")
			{
				products = products.OrderBy(x => (1 - (x.ProductDiscount == null ? 0 : x.ProductDiscount.Percentage) / 100) * (double)x.Price);
			}
			if (!string.IsNullOrEmpty(priceOrder) && priceOrder == "HL")
			{
				products = products.OrderByDescending(x => (1 - (x.ProductDiscount == null ? 0 : x.ProductDiscount.Percentage) / 100) * (double)x.Price);
			}
			// Handle Paginating
			int pageSize = 3;
			return View(PaginatedList<ProductDTO>.Create(products, pageNumber ?? 1, pageSize));
		}
		[HttpGet]
		public async Task<IActionResult> Detail(Guid product_id)
		{
			var product = await _productService.GetProductAsync( u => u.ProductID == product_id, includeProperties:"ProductDiscount");
			var productDTO = _mapper.Map<ProductDTO>(product);
			return View(productDTO);
		}
		public IActionResult Privacy()
		{
			return View();
		}


	}
}