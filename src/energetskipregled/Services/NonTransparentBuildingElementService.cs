using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Common.Helpers;
using EnergetskiPregled.Contracts.Service;
using EnergetskiPregled.Models;
using Microsoft.EntityFrameworkCore;
using EnergetskiPregled.Data;
using EnergetskiPregled.BusinessObjects;
using EnergetskiPregled.Extensions;

namespace EnergetskiPregled.Services
{
	public class NonTrasparentBuildingElemetService : INonTrasparentBuildingElemetService
	{
		private readonly ApplicationDbContext _dbContext;

		public NonTrasparentBuildingElemetService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public NonTrasparentBuildingElemet Get(int id)
		{
			return _dbContext.NonTrasparentBuildingElemets.Where(x => x.Id == id && !x.IsArchived).Include(x => x.MaterialsUsed).ThenInclude(x => x.Material).FirstOrDefault();
		}

		public NonTrasparentBuildingElemet Get(int id, int projectId)
		{
			return _dbContext.NonTrasparentBuildingElemets.Where(x => x.Id == id && !x.IsArchived && x.Project.Id == projectId).Include(x => x.MaterialsUsed).ThenInclude(x => x.Material).FirstOrDefault();
		}


		//public QueryResponse<NonTrasparentBuildingElemet> List(BaseQuery query, int projectId)
		//{
		//	IQueryable<NonTrasparentBuildingElemet> NonTrasparentBuildingElemets = _dbContext.NonTrasparentBuildingElemets
		//		.Include(x => x.Project)
		//		.Where(x => !x.IsArchived && x.ProjectId == projectId);

		//	NonTrasparentBuildingElemets = ApplySortAndOrder(NonTrasparentBuildingElemets, query);

		//	var result = new QueryResponse<NonTrasparentBuildingElemet>
		//	{
		//		Total = NonTrasparentBuildingElemets.Count(),
		//		PageIndex = query.PageValue,
		//		PageSize = query.PageSizeValue,
		//		List = NonTrasparentBuildingElemets.ApplyPaging(query).ToList()
		//	};
		//	return result;
		//}

		public List<NonTrasparentBuildingElemet> ListAll(int projectId)
		{
			IQueryable<NonTrasparentBuildingElemet> devices = _dbContext.NonTrasparentBuildingElemets.Where(x => !x.IsArchived && x.Project.Id == projectId).Include(x => x.MaterialsUsed).ThenInclude(x=>x.Material);
			return devices.ToList();
		}

		public async Task<NonTrasparentBuildingElemet> Create(NonTrasparentBuildingElemet pl)
		{
			if (pl == null)
				throw new ArgumentNullException("Element cannot be null!");

			
			_dbContext.NonTrasparentBuildingElemets.Add(pl);
			await _dbContext.SaveChangesAsync();
			return pl;
		}

	

	
		public async Task<NonTrasparentBuildingElemet> CreateAndAssignToUser(NonTrasparentBuildingElemet nonTrasparentBuildingElemet)
		{
			if (nonTrasparentBuildingElemet == null)
				throw new ArgumentNullException("NonTrasparentBuildingElemet or userId cannot be null!");

			nonTrasparentBuildingElemet.CreatedAt = DateTime.UtcNow;
			nonTrasparentBuildingElemet.LastModifiedAt = nonTrasparentBuildingElemet.CreatedAt;

			return await Create(nonTrasparentBuildingElemet);
		}

		public async Task<NonTrasparentBuildingElemet> Update(NonTrasparentBuildingElemet nonTrasparentBuildingElemet)
		{
			try
			{
				_dbContext.NonTrasparentBuildingElemets.Update(nonTrasparentBuildingElemet);
				await _dbContext.SaveChangesAsync();

			}
			catch (Exception ex)
			{
				var z = 2;
				throw;
			}
			
			return Get(nonTrasparentBuildingElemet.Id);
		}

		public async Task Archive(int id)
		{
			NonTrasparentBuildingElemet ev = Get(id);

			if (ev != null)
			{
				ev.IsArchived = true;
				_dbContext.NonTrasparentBuildingElemets.Update(ev);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task Archive(int[] ids, int projectId)
		{
			List<NonTrasparentBuildingElemet> NonTrasparentBuildingElemets = _dbContext.NonTrasparentBuildingElemets
				.Where(x => ids.Any(y => x.Id == y) && x.Project.Id == projectId).ToList();

			if (NonTrasparentBuildingElemets != null)
			{
				NonTrasparentBuildingElemets.ForEach(x => x.IsArchived = true);
				await _dbContext.SaveChangesAsync();
			}
		}

		#region Private
		private IQueryable<NonTrasparentBuildingElemet> ApplySortAndOrder(IQueryable<NonTrasparentBuildingElemet> events, BaseQuery query)
		{
			switch (query.Order)
			{
				case "name":
					events = events.OrderBy(x => x.Name);
					break;
				case "-name":
					events = events.OrderByDescending(x => x.Name);
					break;
				default:
					events = events.OrderBy(x => x.Name);
					break;
			}

			if (!String.IsNullOrEmpty(query.Filter))
			{
				events = events.Where(s => s.Name.Contains(query.Filter));
			}

			return events;
		}

	
		

		

		#endregion
	}
}
