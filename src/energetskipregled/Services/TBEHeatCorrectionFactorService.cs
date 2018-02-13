using EnergetskiPregled.Contracts.Service;
using System;
using EnergetskiPregled.Models;
using EnergetskiPregled.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergetskiPregled.Services
{
	public class TBEHeatCorrectionFactorService : GenericRepository<TBEHeatCorrectionFactor>, ITBEHeatCorrectionFactorService
	{
		private readonly ApplicationDbContext _dbContext;

		public TBEHeatCorrectionFactorService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public override DbSet<TBEHeatCorrectionFactor> Context()
		{
			return _dbContext.TBEHeatCorrectionFactors;
		}
	}
}