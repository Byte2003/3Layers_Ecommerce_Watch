using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.CategoryDTO
{
	public class CategoryDTO
	{
		public Guid CategoryID { get; set; }

		public string CategoryName { get; set; } = string.Empty;

		public string CategoryDescription { get; set; } = string.Empty;
	}
}
