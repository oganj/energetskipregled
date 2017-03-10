using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Common.Helpers;
using EnergetskiPregled.Contracts.Service;
using EnergetskiPregled.Models;
using Microsoft.EntityFrameworkCore;
using EnergetskiPregled.Data;

namespace EnergetskiPregled.Services
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext _dbContext;

		public UserService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("userService");
			_dbContext = dbContext;
		}

		public async Task<ApplicationUser> GetByIdAsync(string id)
		{
			return await _dbContext.Users.Include(x => x.Players).SingleOrDefaultAsync(x => x.Id.Equals(id));
		}

		public ApplicationUser GetByUsername(string username)
		{
			return _dbContext.Users.Include(x => x.Players).SingleOrDefault(x => x.UserName.Equals(username));
		}

		public async Task<ApplicationUser> GetByUsernameAsync(string username)
		{
			return await _dbContext.Users.Include(x => x.Players).SingleOrDefaultAsync(x => x.UserName.Equals(username));
		}
	}
}
