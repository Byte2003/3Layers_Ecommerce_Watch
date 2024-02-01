using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Watch_Business.DTO.BrandDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
using NuGet.Packaging.Signing;

namespace MVC_Watch_UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]
	public class BrandController : Controller
	{
		private readonly BrandService _brandService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(BrandService brandService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork )
        {
            _brandService = brandService;
			_mapper = mapper;
			_webHostEnvironment = webHostEnvironment;
			_unitOfWork = unitOfWork;
        }
		[HttpGet]
        public async Task<IActionResult> Index()
		{
			var brands = await _brandService.GetAllBrandsAsync();
			return View(brands);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AddBrandDTO brand, IFormFile? file)
		{
			// Handle upload image
			string wwwRootPath = _webHostEnvironment.WebRootPath;
			if (file != null)
			{
				// Generate new guid for image name
				string fileName = Guid.NewGuid().ToString();

				// Combine wwwRootPath with directory images\brands
				var uploads = Path.Combine(wwwRootPath, @"images\brands");

				// Get file extension
				var extension = Path.GetExtension(file.FileName);
				
				// Open stream to upload file
				using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
				{
					file.CopyTo(fileStreams);
				}
				// Set image url to brandDTO model
				brand.BrandImageUrl = @"\images\brands\" + fileName + extension;
			}
			_brandService.AddBrand(brand);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}


		[HttpGet]
		[ActionName("Edit")]
		public async Task<IActionResult> Edit(Guid brand_id)
		{
			var brand =  await _brandService.GetBrandAsync(u => u.BrandID == brand_id);
			var updateBrand = _mapper.Map<UpdateBrandDTO>(brand);
			return View(updateBrand);
		}
		[HttpPost]
		[ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateBrandDTO brand, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				var imgUrl = await _brandService.GetImageUrlOfBrand( u=> u.BrandID == brand.BrandID);
				if (file != null) // User choose new image to update
				{
					string wwwRootPath = _webHostEnvironment.WebRootPath;
					// Generate new guid for image name
					string fileName = Guid.NewGuid().ToString();

					// Combine wwwRootPath with directory images\brands
					var uploads = Path.Combine(wwwRootPath, @"images\brands");
	
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
					brand.BrandImageUrl = @"\images\brands\" + fileName + extension;
				} else
				{
					// Keep the old image					
					brand.BrandImageUrl = imgUrl;
				}			
				_brandService.UpdateBrand(brand);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}
		[HttpGet]
		[ActionName("Delete")]
		public async Task<IActionResult> Delete(Guid brand_id)
		{
			var brand = await _brandService.GetBrandAsync(u => u.BrandID == brand_id);			
			return View(brand);
		}
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(BrandDTO brand)
		{
			
			var oldImageUrl = await _brandService.GetImageUrlOfBrand(u => u.BrandID == brand.BrandID);
			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, oldImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_brandService.DeleteBrand(brand);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		
	}
}
