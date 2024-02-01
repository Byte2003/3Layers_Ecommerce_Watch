using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.DTO.StockDTO;

namespace MVC_Watch_UI.ViewModels
{
	public class StockViewModel
	{
		public StockDTO Stock { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ProductList {  get; set; } 
	}
}
