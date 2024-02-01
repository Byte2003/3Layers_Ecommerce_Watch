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
	public class Product
	{
		[Key]
        public Guid ProductID { get; set; }

		[Required] 
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public decimal Price { get; set; }
		
		public string? ImageUrl { get; set; }

		[Display(Name = "Category")]
		public Guid CategoryID { get; set; }
		[ForeignKey("CategoryID")]
		[ValidateNever]
		public Category Category {  get; set; }

		[Display(Name = "Brand")]
		public Guid BrandID { get; set; }
		[ForeignKey("BrandID")]
		[ValidateNever]
		public Brand Brand { get; set;}

		[ValidateNever]
		public virtual Stock Stock { get; set;}

		[ValidateNever]
		public virtual ProductDiscount ProductDiscount { get; set; }
    }
}
