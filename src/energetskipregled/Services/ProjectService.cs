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
using EnergetskiPregled.Services;

namespace EnergetskiPregled.Services
{
	public class ProjectService : IProjectService
	{
		private readonly ApplicationDbContext _dbContext;

		public ProjectService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public Project Get(int id)
		{
			return _dbContext.Projects
				.Include(x => x.User)
				.SingleOrDefault(x => x.Id == id);
		}

		public Project Get(int id, string userId)
		{
			Project project = Get(id);

			if (project.User.Id == userId)
				return project;
			else
				return null;
		}

		public QueryResponse<Project> List(BaseQuery query, string userId)
		{
			IQueryable<Project> Projects = _dbContext.Projects
				.Include(x => x.User)
				.Where(x => x.User.Id == userId);

			Projects = ApplySortAndOrder(Projects, query);

			var result = new QueryResponse<Project>
			{
				Total = Projects.Count(),
				PageIndex = query.PageValue,
				PageSize = query.PageSizeValue,
				List = Projects.ApplyPaging(query).ToList()
			};
			return result;
		}

		public List<Project> ListAll(string userId)
		{
			IQueryable<Project> devices = _dbContext.Projects.Where(x => x.User.Id == userId);
			return devices.ToList();
		}

		public async Task<Project> Create(Project project)
		{
			if (project == null)
				throw new ArgumentNullException("Event cannot be null!");

			_dbContext.Projects.Add(project);
			await _dbContext.SaveChangesAsync();
			return project;
		}

		public async Task<Project> CreateAndAssignToUser(Project project, string userId)
		{
			if (project == null || String.IsNullOrEmpty(userId))
				throw new ArgumentNullException("Project or userId cannot be null!");

			project.CreatedAt = DateTime.UtcNow;
			project.LastModifiedAt = project.CreatedAt;
			project.UserId = userId;

			return await Create(project);
		}

		public async Task<Project> Update(Project project)
		{
			_dbContext.Projects.Update(project);
			await _dbContext.SaveChangesAsync();

			return Get(project.Id);
		}

		public async Task Archive(int id)
		{
			Project ev = Get(id);

			if (ev != null)
			{
				_dbContext.Projects.Remove(ev);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task Archive(int[] ids, string userId)
		{
			List<Project> projects = _dbContext.Projects
				.Where(x => ids.Any(y => x.Id == y) && x.User.Id == userId).ToList();

			if (projects != null)
			{
				_dbContext.RemoveRange(projects);
				await _dbContext.SaveChangesAsync();
			}
		}

		#region Private
		private IQueryable<Project> ApplySortAndOrder(IQueryable<Project> events, BaseQuery query)
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
