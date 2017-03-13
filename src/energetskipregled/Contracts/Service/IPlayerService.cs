using System.Collections.Generic;
using System.Threading.Tasks;
using EnergetskiPregled.Models;
using EnergetskiPregled.BusinessObjects;

namespace EnergetskiPregled.Contracts.Service
{
	public interface IPlayerService
	{
		Player Get(int id);
		Player Get(int id, string userId);
		QueryResponse<Player> List(BaseQuery query, string userId);
		List<Player> ListAll(string userId);
		Task<Player> Create(Player pl);
		Task<Player> CreateAndAssignToUser(Player pl, string userId);
		Task<Player> Update(Player pl);
		Task Archive(int id);
		Task Archive(int[] ids, string userId);
	}
}
