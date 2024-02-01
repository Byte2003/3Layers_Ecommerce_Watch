using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Models
{
	public class Category
	{
        [Key]
        public Guid CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; } = string.Empty;
    }
}
