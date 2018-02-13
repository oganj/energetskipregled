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
	public class TBEController : BaseMvcController
	{
		private readonly ITBEService _TBEService;
		private readonly IMapperProvider _mapper;

		public TBEController(ITBEService TBEService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (TBEService == null) throw new ArgumentNullException("TBEService");
			_TBEService = TBEService;
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
			List<TBE> result = _TBEService.GetAll();
			var mapper = _mapper.GetMapper<TBE, TBEDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}
	}
}
