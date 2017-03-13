using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Models
{
    public class NonTrasparentBuildingElemet
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Code { get; set; }

		public virtual ICollection<MaterialThickness> MaterialsUsed { get; set; }

		public virtual float InsideTempCondensationCheck { get; set; }
		public virtual float InsideTempTemperatureCheck { get; set; }
		public virtual float Rj { get; set; }
		public virtual float Rm { get; set; }

		public virtual Project Project { get; set; }
		public virtual int ProjectId { get; set; }

		public virtual DateTime CreatedAt { get; set; }
		public virtual DateTime LastModifiedAt { get; set; }
		public virtual bool IsArchived { get; set; }
	}
}
