using EnergetskiPregled.BusinessObjects;
using EnergetskiPregled.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Service
{
 	public interface IMaterialService
	{
		List<Material> ListAll();
		Material Get(int id);
		Task<Material> Create(Material pl);
		Task<Material> Update(Material pl);
		Task Remove(int id);
	}
}
