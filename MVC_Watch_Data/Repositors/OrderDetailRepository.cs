using MVC_Watch_Data.Data;
using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Repositors
{
	public class OrderDetailRepository : Repository<OrderDetail>
	{
		private readonly AppDbContext _db;
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
