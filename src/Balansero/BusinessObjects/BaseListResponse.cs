using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.BusinessObjects
{
	public class BaseListResponse
	{
		public int Total { get; set; }

		/// <summary>
		/// between 5 ~ 200
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Start from 1
		/// </summary>
		public int PageIndex { get; set; }
	}
}
