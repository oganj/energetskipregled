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
	public class TBEFrameController : BaseMvcController
	{
		private readonly ITBEFrameService _TBEFrameService;
		private readonly IMapperProvider _mapper;

		public TBEFrameController(ITBEFrameService TBEFrameService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (TBEFrameService == null) throw new ArgumentNullException("TBEFrameService");
			_TBEFrameService = TBEFrameService;
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
			List<TBEFrame> result = _TBEFrameService.GetAll();
			var mapper = _mapper.GetMapper<TBEFrame, TBEFrameDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}
	}
}
