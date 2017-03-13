using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.BusinessObjects
{
	public class BaseQueryResponse<T> : BaseListResponse
	{
		public BaseQueryResponse()
		{
			List = new List<T>();
		}

		public List<T> List { get; set; }
	}
}
