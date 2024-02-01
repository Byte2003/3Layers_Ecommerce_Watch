using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.DTO.BrandDTO;
using MVC_Watch_Business.DTO.CategoryDTO;
using MVC_Watch_Business.DTO.ProductDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using MVC_Watch_UI.Utils;
using MVC_Watch_UI.ViewModels;

namespace MVC_Watch_UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class ProductController : Controller
	{
		private readonly ProductService _productService;
		private readonly BrandService _brandService;
		private readonly CategoryService _categoryService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(ProductService productService, BrandService brandService, CategoryService categoryService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
		{
			_productService = productService;
			_brandService = brandService;
			_categoryService = categoryService;
			_webHostEnvironment = webHostEnvironment;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var productsDomain = await _productService.GetAllProductsAsync(includeProperties: "Category,Brand");
			var products = _mapper.Map<IEnumerable<ProductDTO>>(productsDomain);
			return View(products);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			var brands = await _brandService.GetAllBrandsAsync();
			ProductViewModel productVM = new ProductViewModel
			{
				Product = new(),
				CategoryList = categories.Select(i => new SelectListItem
				{
					Text = i.CategoryName,
					Value = i.CategoryID.ToString()
				}),
				BrandList = brands.Select(i => new SelectListItem
				{
					Text = i.BrandName,
					Value = i.BrandID.ToString()
				}),
			};
			return View(productVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductViewModel productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				// Handle upload image
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					// Generate new guid for image name
					string fileName = Guid.NewGuid().ToString();

					// Combine wwwRootPath with directory images\products
					var uploads = Path.Combine(wwwRootPath, @"images\products");

					// Get file extension
					var extension = Path.GetExtension(file.FileName);

					// Open stream to upload file
					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStreams);
					}
					// Set image url to brandDTO model
					productVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
				_productService.AddProduct(productVM.Product);
				_unitOfWork.Save();

			}
			return RedirectToAction("Index");

		}
		[HttpGet]
		public async Task<IActionResult> Update(Guid product_id)
		{
			var product = await _productService.GetProductAsync(u => u.ProductID == product_id);
			var updateProduct = _mapper.Map<UpdateProductDTO>(product);
			var categories = await _categoryService.GetAllCategoriesAsync();
			var brands = await _brandService.GetAllBrandsAsync();
			UpdateProductViewModel productViewModel = new()
			{
				Product = updateProduct,
				CategoryList = categories.Select(i => new SelectListItem
				{
					Text = i.CategoryName,
					Value = i.CategoryID.ToString()
				}),
				BrandList = brands.Select(i => new SelectListItem
				{
					Text = i.BrandName,
					Value = i.BrandID.ToString()
				}),
			};
			return View(productViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(UpdateProductViewModel updateProductViewModel, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				// Get the old image of product
				var imgUrl = await _productService.GetImageUrlOfProduct(u => u.ProductID == updateProductViewModel.Product.ProductID);
				if (file != null) // User choose new image to update
				{
					string wwwRootPath = _webHostEnvironment.WebRootPath;
					// Generate new guid for image name
					string fileName = Guid.NewGuid().ToString();

					// Combine wwwRootPath with directory images\brands
					var uploads = Path.Combine(wwwRootPath, @"images\products");

					// Get file extension
					var extension = Path.GetExtension(file.FileName);

					var oldImagePath = Path.Combine(wwwRootPath, imgUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
					// Open stream to upload file
					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStreams);
					}
					// Set image url to brandDTO model
					updateProductViewModel.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
				else
				{
					// Keep the old image					
					updateProductViewModel.Product.ImageUrl = imgUrl;
				}
				_productService.UpdateProduct(updateProductViewModel.Product);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}
		[HttpGet]
		public async Task<IActionResult> Delete(Guid product_id)
		{
			var product = await _productService.GetProductAsync(u => u.ProductID == product_id);
			return View(product);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(ProductDTO product)
		{
			var oldImageUrl = await _productService.GetImageUrlOfProduct(u => u.ProductID == product.ProductID);
			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, oldImageUrl.TrimStart('\\'));
			_productService.DeleteProduct(product);
			_unitOfWork.Save();
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			return RedirectToAction("Index");
		}
	}
}
