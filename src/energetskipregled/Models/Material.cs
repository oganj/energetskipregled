using System.Collections.Generic;

namespace EnergetskiPregled.Models
{
    public class Material
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual MaterialCategory Category { get; set; }
		public virtual ICollection<MaterialThickness> BuildingElements { get; set; }
		public virtual int CategoryId { get; set; }
		public virtual int Density { get; set; }
		public virtual float HeatConduction { get; set; }
		public virtual int SpecificHeat { get; set; }
		public virtual float RelativeWaterVaporDiffusionCoefficient { get; set; }
		public virtual bool IsArchived { get; set; }
	}
}
