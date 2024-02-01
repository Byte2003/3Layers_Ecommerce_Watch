using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.BrandDTO
{
	public class BrandDTO
	{
		public Guid BrandID { get; set; }

		public string BrandName { get; set; }

		public string BrandDescription { get; set; }

		[ValidateNever]
		public string? BrandImageUrl { get; set; }
	}
}
