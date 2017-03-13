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
	public class PlayerService : IPlayerService
	{
		private readonly ApplicationDbContext _dbContext;

		public PlayerService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public Player Get(int id)
		{
			return _dbContext.Players
				.Include(x => x.User)
				.Include(x => x.LastModifiedBy)
				.Include(x => x.CreatedBy)
				.SingleOrDefault(x => x.Id == id && !x.IsArchived);
		}

		public Player Get(int id, string userId)
		{
			Player player = Get(id);

			if (player.User.Id == userId)
				return player;
			else
				return null;
		}

		public QueryResponse<Player> List(BaseQuery query, string userId)
		{
			IQueryable<Player> players = _dbContext.Players
				.Include(x => x.LastModifiedBy)
				.Include(x => x.CreatedBy)
				.Include(x => x.User)
				.Where(x => !x.IsArchived && x.User.Id == userId);

			players = ApplySortAndOrder(players, query);

			var result = new QueryResponse<Player>
			{
				Total = players.Count(),
				PageIndex = query.PageValue,
				PageSize = query.PageSizeValue,
				List = players.ApplyPaging(query).ToList()
			};
			return result;
		}

		public List<Player> ListAll(string userId)
		{
			IQueryable<Player> devices = _dbContext.Players.Where(x => !x.IsArchived && x.User.Id == userId);
			return devices.ToList();
		}

		public async Task<Player> Create(Player player)
		{
			if (player == null)
				throw new ArgumentNullException("Event cannot be null!");
			
			_dbContext.Players.Add(player);
			await _dbContext.SaveChangesAsync();
			return player;
		}

		public async Task<Player> CreateAndAssignToUser(Player player, string userId)
		{
			if (player == null || String.IsNullOrEmpty(userId))
				throw new ArgumentNullException("Player or userId cannot be null!");

			player.CreatedAt = DateTime.UtcNow;
			player.LastModifiedAt = player.CreatedAt;
			player.CreatedById = userId;
			player.LastModifiedById = userId;
			player.UserId = userId;

			return await Create(player);
		}

		public async Task<Player> Update(Player player)
		{
			_dbContext.Players.Update(player);
			await _dbContext.SaveChangesAsync();

			return Get(player.Id);
		}

		public async Task Archive(int id)
		{
			Player ev = Get(id);

			if (ev != null)
			{
				ev.IsArchived = true;
				_dbContext.Players.Update(ev);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task Archive(int[] ids, string userId)
		{
			List<Player> players = _dbContext.Players
				.Where(x => ids.Any(y => x.Id == y) && x.User.Id == userId).ToList();

			if (players != null)
			{
				players.ForEach(x => x.IsArchived = true);
				await _dbContext.SaveChangesAsync();
			}
		}

		#region Private
		private IQueryable<Player> ApplySortAndOrder(IQueryable<Player> events, BaseQuery query)
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
