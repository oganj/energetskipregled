using EnergetskiPregled.Contracts.Service;
using System;
using EnergetskiPregled.Models;
using EnergetskiPregled.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergetskiPregled.Services
{
	public class TBEService : GenericRepository<TBE>, ITBEService
	{
		private readonly ApplicationDbContext _dbContext;

		public TBEService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public override DbSet<TBE> Context()
		{
			return _dbContext.TBEs;
		}
	}
}