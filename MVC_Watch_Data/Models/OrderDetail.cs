using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Models
{
	public class OrderDetail
	{
		[Key]
		public Guid OrderDetailID {  get; set; }

		[Required]
		public Guid OrderHeaderID { get; set; }
		[ForeignKey("OrderHeaderID")]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }

		[Required]
		public Guid ProductID { get; set; }
		[ForeignKey("ProductID")]
		[ValidateNever]
		public Product Product { get; set; }

		public int Quantity { get; set; }
		public double Price { get; set; }


	}
}
