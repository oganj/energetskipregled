using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balansero.Contracts.Data
{
    public class MaterialDto
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual MaterialCategoryDto Category { get; set; }
		public virtual ICollection<MaterialThicknessDto> BuildingElements { get; set; }
		public virtual int CategoryId { get; set; }
		public virtual int Density { get; set; }
		public virtual float HeatConduction { get; set; }
		public virtual int SpecificHeat { get; set; }
		public virtual float RelativeWaterVaporDiffusionCoefficient { get; set; }
		public virtual bool IsArchived { get; set; }
	}
}
