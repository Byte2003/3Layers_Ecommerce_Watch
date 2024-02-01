
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.DTO.BrandDTO;
using MVC_Watch_Business.DTO.CategoryDTO;
using MVC_Watch_Business.DTO.ProductDTO;

namespace MVC_Watch_UI.ViewModels
{
	public class UpdateProductViewModel
	{
		public UpdateProductDTO Product { get; set; }
		[ValidateNever]
        public IEnumerable<SelectListItem> BrandList { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
