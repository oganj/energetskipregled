using EnergetskiPregled.Contracts.Service;
using System;
using EnergetskiPregled.Models;
using EnergetskiPregled.Data;
using Microsoft.EntityFrameworkCore;

namespace EnergetskiPregled.Services
{
	public class TBEMaterialService : GenericRepository<TBEMaterial>, ITBEMaterialService
	{
		private readonly ApplicationDbContext _dbContext;

		public TBEMaterialService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public override DbSet<TBEMaterial> Context()
		{
			return _dbContext.TBEMaterials;
		}
	}
}