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
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
			Category = new CategoryRepository(_db);
			Brand = new BrandRepository(_db);
			Product = new ProductRepositoty(_db);
			ProductDiscount = new ProductDiscountRepository(_db);
			Stock = new StockRepository(_db);
			CartItem = new CartItemRepository(_db);	
			OrderHeader = new OrderHeaderRepository(_db);
			OrderDetail = new OrderDetailRepository(_db);
        }
        public IRepository<Category> Category {  get; private set; }
		public IRepository<Brand> Brand { get; private set; }
		public IRepository<Product> Product { get; private set; }
		public IRepository<ProductDiscount> ProductDiscount { get; private set; }
		public IRepository<Stock> Stock { get; private set; }
		public IRepository<CartItem> CartItem { get; private set; }
		public IRepository<OrderDetail> OrderDetail { get; private set; }
        public IRepository<OrderHeader> OrderHeader { get; private set; }
        public void Save()
		{
			_db.SaveChanges();
		}
	}
}
