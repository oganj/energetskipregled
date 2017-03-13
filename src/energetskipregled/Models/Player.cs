using System;
using System.Collections.Generic;

namespace EnergetskiPregled.Models
{
	public class Player
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool IsArchived { get; set; }
		public virtual DateTime CreatedAt { get; set; }
		public virtual string CreatedById { get; set; }
		public virtual ApplicationUser CreatedBy { get; set; }
		public virtual DateTime LastModifiedAt { get; set; }
		public virtual string LastModifiedById { get; set; }
		public virtual ApplicationUser LastModifiedBy { get; set; }
		public virtual float Skill { get; set; }

		public virtual ApplicationUser User { get; set; }
		public virtual string UserId { get; set; }
	}
}
