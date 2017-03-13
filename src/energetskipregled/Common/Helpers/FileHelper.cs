using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Common.Helpers
{
	public static class TypeHelper
	{
		public static string GetTypesKey<T1, T2>()
		{
			return String.Format("{0}{1}", typeof(T1).FullName, typeof(T2));
		}
	}
}
