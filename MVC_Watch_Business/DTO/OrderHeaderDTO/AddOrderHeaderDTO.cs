using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.OrderHeaderDTO
{
	public class AddOrderHeaderDTO
	{
		[Key]
		public Guid OrderHeaderId { get; set; }

		public string AppUserID { get; set; }
		//[ForeignKey("AppUserID")]
		//[ValidateNever]
		//public AppUser AppUser { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }
		public DateTime ShippingDate { get; set; }
		public double OrderTotal { get; set; }

		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
