using EnergetskiPregled.BusinessObjects;
using EnergetskiPregled.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Service
{
 	public interface IMaterialCategoryService
	{
		List<MaterialCategory> ListAll();
		MaterialCategory Get(int id);
		Task<MaterialCategory> Create(MaterialCategory pl);
		Task<MaterialCategory> Update(MaterialCategory pl);
		Task Remove(int id);
	}
}
