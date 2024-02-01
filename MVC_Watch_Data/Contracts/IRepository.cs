using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Contracts
{
	public interface IRepository<T> where T : class
	{
		public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,string? includeProperties = null);
		public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
		public void Add(T _object);
		public void Delete(T _object);
		public void Update(T _object);
		
	}
}
