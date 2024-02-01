using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.ProductDTO
{
	public class AddProductDTO
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		[ValidateNever]
		public string? ImageUrl { get; set; }

		[Display(Name = "Category")]
		public Guid CategoryID { get; set; }

		// Don't need it 
		//[ForeignKey("CategoryID")]
		//[ValidateNever]
		//public Category Category { get; set; }

		[Display(Name = "Brand")]
		public Guid BrandID { get; set; }
		// Don't need it 
		//[ForeignKey("BrandID")]
		//[ValidateNever]
		//public Brand Brand { get; set; }

		[ValidateNever]
		public virtual Stock Stock { get; set; }

		[ValidateNever]
		public virtual ProductDiscount ProductDiscount { get; set; }
	}
}
