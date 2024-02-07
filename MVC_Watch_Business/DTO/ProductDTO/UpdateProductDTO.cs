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
	public class UpdateProductDTO
	{
		[Key]
		public Guid ProductID { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }

		[ValidateNever]
		public string? ImageUrl { get; set; }

		[ValidateNever]
		public Guid CategoryID { get; set; }

		[ValidateNever]
		public Guid BrandID { get; set; }
	}
}
