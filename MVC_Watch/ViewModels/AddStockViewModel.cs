using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Watch_Business.DTO.StockDTO;
using MVC_Watch_Data.Models;

namespace MVC_Watch_UI.ViewModels
{
	public class AddStockViewModel
	{
		public AddStockDTO Stock { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ProductList {  get; set; } 
	}
}
