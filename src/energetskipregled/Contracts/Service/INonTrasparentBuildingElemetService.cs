using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Models;
using EnergetskiPregled.BusinessObjects;

namespace EnergetskiPregled.Contracts.Service
{
	public interface INonTrasparentBuildingElemetService
	{
		NonTrasparentBuildingElemet Get(int id);
		NonTrasparentBuildingElemet Get(int id, int projectId);
		//QueryResponse<NonTrasparentBuildingElemet> List(BaseQuery query, int projectId);
		List<NonTrasparentBuildingElemet> ListAll(int projectId);
		Task<NonTrasparentBuildingElemet> Create(NonTrasparentBuildingElemet pl);
		Task<NonTrasparentBuildingElemet> CreateAndAssignToUser(NonTrasparentBuildingElemet pl);
		Task<NonTrasparentBuildingElemet> Update(NonTrasparentBuildingElemet pl);
		Task Archive(int id);
		Task Archive(int[] ids, int projectId);
	}
}
