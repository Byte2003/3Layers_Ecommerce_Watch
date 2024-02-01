using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.ProductDiscountDTO
{
	public class UpdateProductDiscountDTO
	{
		[Key]
		public Guid PDiscountID { get; set; }

		[Display(Name = "Product")]
		public Guid ProductID { get; set; }

		[ForeignKey("ProductID")]
		[ValidateNever]
		public virtual Product Product { get; set; }

		public double Percentage { get; set; }
	}
}
