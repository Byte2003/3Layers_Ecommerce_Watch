using MVC_Watch_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Contracts
{
	public interface IUnitOfWork
	{
		IRepository<Category> Category {  get; }
		IRepository<Brand > Brand { get; }
		IRepository<Product> Product { get; }
		IRepository<ProductDiscount> ProductDiscount { get; }
		IRepository<Stock> Stock { get; }
		IRepository<CartItem> CartItem { get; }
		IRepository<OrderHeader> OrderHeader { get; }
		IRepository<OrderDetail> OrderDetail { get; }
		void Save();
	}
}
