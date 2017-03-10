using EnergetskiPregled.BusinessObjects;
using System.Linq;

namespace EnergetskiPregled.Extensions
{
	public static class QueryExtensions
	{
		public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> queryable, BaseQuery paging)
		{
			if (paging.PageSizeValue > 0)
			{
				queryable = queryable.Skip(paging.SkipCount).Take(paging.PageSizeValue);
			}
			return queryable;
		}
	}
}