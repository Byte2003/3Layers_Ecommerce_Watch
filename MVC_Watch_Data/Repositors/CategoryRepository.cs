using Microsoft.EntityFrameworkCore;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Data;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Repositors
{
	public class CategoryRepository : Repository<Category>
	{
		private readonly AppDbContext _db;

		public CategoryRepository(AppDbContext db): base(db) 
        {
			_db = db;
		}       

	}
}
