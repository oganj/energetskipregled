using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.BusinessObjects
{
	public class QueryResponse<T>
	{
		public List<T> List { get; set; }
		public int Total { get; set; }

		/// <summary>
		/// between 5 ~ 200
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Start from 1
		/// </summary>
		public int PageIndex { get; set; }

		public bool All { get; set; }
	}
}
