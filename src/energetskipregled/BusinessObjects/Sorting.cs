using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.BusinessObjects
{
	public class Sorting
	{
		public Sorting()
		{
			Asc = true;
		}

		public string Field { get; set; }
		public bool Asc { get; set; }
		public override string ToString()
		{
			if (Asc)
			{
				return Field;
			}
			return String.Format("{0} desc", Field);
		}
	}
}
