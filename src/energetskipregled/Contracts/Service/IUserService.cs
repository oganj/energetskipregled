using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Models;

namespace EnergetskiPregled.Contracts.Service
{
	public interface IUserService
	{
		Task<ApplicationUser> GetByIdAsync(string id);
		Task<ApplicationUser> GetByUsernameAsync(string username);
		ApplicationUser GetByUsername(string username);
	}
}
