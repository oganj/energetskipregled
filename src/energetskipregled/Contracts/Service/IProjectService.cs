using EnergetskiPregled.BusinessObjects;
using EnergetskiPregled.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Service
{
 	public interface IProjectService
	{
		Project Get(int id);
		Project Get(int id, string userId);
		QueryResponse<Project> List(BaseQuery query, string userId);
		List<Project> ListAll(string userId);
		Task<Project> Create(Project pl);
		Task<Project> CreateAndAssignToUser(Project pl, string userId);
		Task<Project> Update(Project pl);
		Task Archive(int id);
		Task Archive(int[] ids, string userId);
	}
}
