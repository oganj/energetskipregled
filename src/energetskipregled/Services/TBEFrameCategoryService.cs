using EnergetskiPregled.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EnergetskiPregled.Data;

namespace EnergetskiPregled.Services
{
	public class TBEFrameCategoryService : GenericRepository<TBEFrameCategory>, ITBEFrameCategoryService
	{
		private readonly ApplicationDbContext _dbContext;

		public TBEFrameCategoryService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public override DbSet<TBEFrameCategory> Context()
		{
			return _dbContext.TBEFrameCategorys;
		}
	}
}
