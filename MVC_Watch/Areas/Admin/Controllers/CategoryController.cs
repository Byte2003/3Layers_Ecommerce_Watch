using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Watch_Business.DTO.CategoryDTO;
using MVC_Watch_Business.Services;
using MVC_Watch_Data.Contracts;
namespace MVC_Watch_UI.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "admin")]

	public class CategoryController : Controller
	{
		private readonly CategoryService _categoryService;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(CategoryService categoryService, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_categoryService = categoryService;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetAllCategoriesAsync();
			return View(categories);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AddCateDTO addCateDTO)
		{
			if (ModelState.IsValid)
			{
				_categoryService.AddCategory(addCateDTO);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid cate_id)
		{
			var cate = await _categoryService.GetCategoryAsync(u => u.CategoryID == cate_id);
			var updateCate = _mapper.Map<UpdateCateDTO>(cate);
			return View(updateCate);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(UpdateCateDTO updateCateDTO)
		{
			if (ModelState.IsValid)
			{
				_categoryService.UpdateCategory(updateCateDTO);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid cate_id)
		{
			var domainCate = await _categoryService.GetCategoryAsync(u => u.CategoryID == cate_id);
			var cate = _mapper.Map<CategoryDTO>(domainCate);
			return View(cate);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(CategoryDTO category)
		{
			_categoryService.DeleteCategory(category);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
