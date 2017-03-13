using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Data
{
	public class PlayerDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public UserDto CreatedBy { get; set; }
		public DateTime LastModifiedAt { get; set; }
		public UserDto LastModifiedBy { get; set; }
		public DateTime DateScheduled { get; set; }
		public float Skill { get; set; }
		public UserDto User { get; set; }
	}
}
