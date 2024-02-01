using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Business.DTO.AppUserDTO
{
	public class AppUserDTO : IdentityUser
	{
		public string? FirstName { get; set; } = null;
		public string? LastName { get; set; } = null;
		public string? Address { get; set; } = null;
		public string? City { get; set; } = null;
		public string? PostalCode { get; set; } = null;
	}
}
