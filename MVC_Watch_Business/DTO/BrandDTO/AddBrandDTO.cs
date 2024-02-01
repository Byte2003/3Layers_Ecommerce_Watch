using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.BrandDTO
{
	public class AddBrandDTO
	{
		[Required]
		public string BrandName { get; set; }

		[Required]
		public string BrandDescription { get; set; }

		[ValidateNever]
		public string? BrandImageUrl { get; set; } 
	}
}
