using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.BrandDTO
{
	public class UpdateBrandDTO
	{
		public Guid BrandID { get; set; }

		[Required]
		public string BrandName { get; set; } = string.Empty;

		[Required]
		public string BrandDescription { get; set; } = string.Empty;

		[ValidateNever]
		public string? BrandImageUrl { get; set; } = string.Empty;
	}
}
