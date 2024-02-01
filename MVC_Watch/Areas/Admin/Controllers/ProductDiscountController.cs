using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.DTO.ProductDiscountDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using MVC_Watch_UI.ViewModels;
using System.ComponentModel;

namespace MVC_Watch_UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class ProductDiscountController : Controller
	{
		private readonly ProductDiscountService _productDiscountService;
		private readonly ProductService _productService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ProductDiscountController(ProductDiscountService productDiscountService, ProductService productService, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_productDiscountService = productDiscountService;
			_productService = productService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var productDiscounts = await _productDiscountService.GetAllProductDiscountsAsync(includeProperties: "Product");
			return View(productDiscounts);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var products = await _productService.GetAllProductsAsync();
			var productDiscountVM = new AddProductDiscountViewModel
			{
				ProductDiscount = new(),
				ProductList = products.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.ProductID.ToString()
				}),
			};
			return View(productDiscountVM);
		}
		[HttpPost]
		[ActionName("Create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AddProductDiscountViewModel model)
		{
			if (ModelState.IsValid)
			{
				_productDiscountService.AddProductDiscount(model.ProductDiscount);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid discount_id)
		{
			var productDiscount = await _productDiscountService.GetProductDiscountAsync(u => u.PDiscountID == discount_id);
			var updateProductDiscount = _mapper.Map<UpdateProductDiscountDTO>(productDiscount);
			var products = await _productService.GetAllProductsAsync();
			var productDiscountVM = new UpdateProductDiscountViewModel
			{
				ProductDiscount = updateProductDiscount,
				ProductList = products.Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.ProductID.ToString()
				}),
			};
			return View(productDiscountVM);
		}
		[HttpPost]
		[ActionName("Update")]
		[ValidateAntiForgeryToken]
		public IActionResult Update(UpdateProductDiscountViewModel model)
		{
			if (ModelState.IsValid)
			{
				_productDiscountService.UpdateProductDiscount(model.ProductDiscount);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}

		#region API Calls
		[HttpGet]
		public async Task<IActionResult> GetAllDiscounts()
		{
			var productDiscounts = await _productDiscountService.GetAllProductDiscountsAsync(includeProperties: "Product");
			return Json(new { data = productDiscounts });
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(Guid discount_id)
		{
			var pDiscount = await _productDiscountService.GetProductDiscountAsync(u => u.PDiscountID.Equals(discount_id));
			if (pDiscount == null)
			{
				// More code
			}
			else
			{
				_productDiscountService.DeleteProductDiscount(pDiscount);
				_unitOfWork.Save();
			}
			return Json(new { success = true, message = "Delete successfully" });
		}

		#endregion
	}
}
