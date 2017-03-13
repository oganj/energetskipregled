using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Data
{
    public class MaterialThicknessDto
    {
		public virtual int Id { get; set; }
		public virtual NonTrasparentBuildingElemetDto BuildingElement { get; set; }
		public virtual int BuildingElementId { get; set; }
		public virtual MaterialDto Material { get; set; }
		public virtual int MaterialId { get; set; }
		public virtual float Thickness { get; set; }
	}
}
