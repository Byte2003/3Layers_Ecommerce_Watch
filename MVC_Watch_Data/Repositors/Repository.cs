using Microsoft.EntityFrameworkCore;
using MVC_Watch_Data.Contracts;
using MVC_Watch_Data.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Watch_Data.Repositors
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
			_dbSet = _db.Set<T>();
        }
        public void Add(T _object)
		{
			_dbSet.Add(_object);
		}

		public void Delete(T _object)
		{
			_dbSet.Remove(_object);
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
			{
				query = query.Where(filter);
			}
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.ToListAsync();
		}

		public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = _dbSet;
			query = query.Where(filter);
			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.FirstOrDefaultAsync();
		}

		public void Update(T _object)
		{
			_db.Update(_object);
		}
	}
}
