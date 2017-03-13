using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Data
{
	public class BaseApiResponse
	{
		public int Ack { get; set; }
		public string Description { get; set; }
	}
}
