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
		NonTrasparentBuildingElemet Get(int id, int projectId, string userId);
		QueryResponse<NonTrasparentBuildingElemet> List(BaseQuery query, int projectId, string userId);
		List<NonTrasparentBuildingElemet> ListAll(int projectId, string userId);
		Task<NonTrasparentBuildingElemet> Create(NonTrasparentBuildingElemet pl, int projectId);
		Task<NonTrasparentBuildingElemet> CreateAndAssignToUser(NonTrasparentBuildingElemet pl, string userId);
		Task<NonTrasparentBuildingElemet> Update(NonTrasparentBuildingElemet pl);
		Task Archive(int id);
		Task Archive(int[] ids, string userId);
	}
}
