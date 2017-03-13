using EnergetskiPregled.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balansero.Contracts.Data
{
    public class ProjectDto
    {
		public virtual int Id { get; set; }
		public virtual int Name { get; set; }
		public virtual DateTime CreatedAt { get; set; }
		public virtual DateTime LastModifiedAt { get; set; }

		public virtual ICollection<NonTrasparentBuildingElemetDto> NonTransparentBuildingElements { get; set; }

		public virtual UserDto User { get; set; }
		public virtual string UserId { get; set; }
	}
}
