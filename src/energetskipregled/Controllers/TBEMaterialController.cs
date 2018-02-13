using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EnergetskiPregled.Models;
using System.Threading.Tasks;
using EnergetskiPregled.Contracts.Service;
using EnergetskiPregled.Contracts.Mappers;
using System;
using EnergetskiPregled.BusinessObjects;
using EnergetskiPregled.Contracts.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergetskiPregled.Controllers
{
	[Authorize]
	public class TBEMaterialController : BaseMvcController
	{
		private readonly ITBEMaterialService _tbeMaterialService;
		private readonly IMapperProvider _mapper;

		public TBEMaterialController(ITBEMaterialService tbeMaterialService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (tbeMaterialService == null) throw new ArgumentNullException("TBEMaterialService");
			_tbeMaterialService = tbeMaterialService;
			_mapper = mapper;
		}

		// GET: /<controller>/
		public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public async Task<ActionResult> ListAll()
		{
			List<TBEMaterial> result = _tbeMaterialService.GetAll();
			var mapper = _mapper.GetMapper<TBEMaterial, TBEMaterialDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}
	}
}
