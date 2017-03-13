using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.BusinessObjects
{
	public class BaseQuery
	{
		public int? Page { get; set; }
		public int? PageSize { get; set; }
		public string Order { get; set; }
		public string Filter { get; set; }

		public int PageValue
		{
			get
			{
				return Page ?? 1;
			}
		}

		public int PageSizeValue
		{
			get
			{
				return PageSize ?? 25;
			}
		}

		public int SkipCount
		{
			get
			{
				return (PageValue - 1) * PageSizeValue;
			}
		}

		public void FixPageForCount(int count)
		{
			if (count <= SkipCount)
			{
				Page = 1;
			}
		}

		public bool? Asc
		{
			get
			{
				if (String.IsNullOrEmpty(Order))
					return null;
				return !Order.StartsWith("-");
			}
		}
	}
}
