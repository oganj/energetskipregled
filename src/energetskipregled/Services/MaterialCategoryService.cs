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
	public class MaterialCategoryService : IMaterialCategoryService
	{
		private readonly ApplicationDbContext _dbContext;

		public MaterialCategoryService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public MaterialCategory Get(int id)
		{
			return _dbContext.MaterialCategorys
				
				.SingleOrDefault(x => x.Id == id);
		}

		public List<MaterialCategory> ListAll()
		{
			return _dbContext.MaterialCategorys.ToList();
		}

		public async Task<MaterialCategory> Create(MaterialCategory elem)
		{
			if (elem == null)
				throw new ArgumentNullException("Element on create cannot be null!");

			_dbContext.MaterialCategorys.Add(elem);
			await _dbContext.SaveChangesAsync();
			return elem;
		}

		public async Task<MaterialCategory> Update(MaterialCategory elem)
		{
			_dbContext.MaterialCategorys.Update(elem);
			await _dbContext.SaveChangesAsync();

			return Get(elem.Id);
		}
		
		public async Task Remove(int id)
		{
			MaterialCategory ev = Get(id);

			if (ev != null)
			{
				_dbContext.MaterialCategorys.Remove(ev);
				await _dbContext.SaveChangesAsync();
			}
		}

		
	}
}
