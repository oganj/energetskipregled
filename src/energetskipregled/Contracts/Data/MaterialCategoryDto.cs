using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Data
{
    public class MaterialCategoryDto
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool IsArchived { get; set; }
	}
}
