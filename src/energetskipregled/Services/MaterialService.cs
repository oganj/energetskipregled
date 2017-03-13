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
	public class MaterialService : IMaterialService
	{
		private readonly ApplicationDbContext _dbContext;

		public MaterialService(ApplicationDbContext dbContext)
		{
			if (dbContext == null) throw new ArgumentNullException("dbContext");
			_dbContext = dbContext;
		}

		public Material Get(int id)
		{
			return _dbContext.Materials
				.SingleOrDefault(x => x.Id == id);
		}

		public List<Material> ListAll()
		{
			return _dbContext.Materials.ToList();
		}

		public async Task<Material> Create(Material elem)
		{
			if (elem == null)
				throw new ArgumentNullException("Element on create cannot be null!");

			_dbContext.Materials.Add(elem);
			await _dbContext.SaveChangesAsync();
			return elem;
		}

		public async Task<Material> Update(Material elem)
		{
			_dbContext.Materials.Update(elem);
			await _dbContext.SaveChangesAsync();

			return Get(elem.Id);
		}
		
		public async Task Remove(int id)
		{
			Material ev = Get(id);

			if (ev != null)
			{
				_dbContext.Materials.Remove(ev);
				await _dbContext.SaveChangesAsync();
			}
		}

		
	}
}
