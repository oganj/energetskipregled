using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Service
{
	public interface IGenericRepository<T> where T : class
	{
		List<T> GetAll();		
		IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
		void Create(T entity);
		void Remove(T entity);
		void Update(T entity);
	}
}
