using EnergetskiPregled.Contracts.Service;
using System;
using EnergetskiPregled.Models;
using EnergetskiPregled.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergetskiPregled.Services
{
	public class TBEFrameService : GenericRepository<TBEFrame>, ITBEFrameService
	{
		private readonly ApplicationDbContext _dbContext;

		public TBEFrameService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public override DbSet<TBEFrame> Context()
		{
			return _dbContext.TBEFrames;
		}
	}
}
