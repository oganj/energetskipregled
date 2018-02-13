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
	public class TBEHeatCorrectionFactorController : BaseMvcController
	{
		private readonly ITBEHeatCorrectionFactorService _TBEHeatCorrectionFactorService;
		private readonly IMapperProvider _mapper;

		public TBEHeatCorrectionFactorController(ITBEHeatCorrectionFactorService TBEHeatCorrectionFactorService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (TBEHeatCorrectionFactorService == null) throw new ArgumentNullException("TBEHeatCorrectionFactorService");
			_TBEHeatCorrectionFactorService = TBEHeatCorrectionFactorService;
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
			List<TBEHeatCorrectionFactor> result = _TBEHeatCorrectionFactorService.GetAll();
			var mapper = _mapper.GetMapper<TBEHeatCorrectionFactor, TBEHeatCorrectionFactorDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}
	}
}
