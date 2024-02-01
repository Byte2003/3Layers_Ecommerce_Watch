using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.CartItemDTO
{
	public class CartItemDTO
	{
		[Key]
		public Guid CartItemID { get; set; }

		public Guid ProductID { get; set; }
		[ForeignKey("ProductID")]
		[ValidateNever]
		public Product Product { get; set; }

		[Range(0, 100)]
		public int Quantity { get; set; }

		public string AppUserID { get; set; }
		[ForeignKey("AppUserID")]
		[ValidateNever]
		public AppUser AppUser { get; set; }

		[NotMapped]
		public double Price { get; set; }
	}
}
