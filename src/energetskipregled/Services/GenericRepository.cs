using EnergetskiPregled.Contracts.Service;
using EnergetskiPregled.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Services
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, new()
	{	
		public abstract DbSet<T> Context();

		public virtual List<T> GetAll()
		{ 
			return Context().ToList();
		}

		public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = Context().Where(predicate);
			return query;
		}

		public void Create(T entity)
		{
			Context().Add(entity);
		}

		public virtual void Remove(T entity)
		{
			Context().Remove(entity);
		}

		public virtual void Update(T entity)
		{
			Context().Update(entity);
		}
	}
}
