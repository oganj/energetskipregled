using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Models
{
    public class Project
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual DateTime CreatedAt { get; set; }
		public virtual DateTime LastModifiedAt { get; set; }

		public virtual ICollection<NonTrasparentBuildingElemet> NonTransparentBuildingElements { get; set; }

		public virtual ApplicationUser User { get; set; }
		public virtual string UserId { get; set; }
	}
}
