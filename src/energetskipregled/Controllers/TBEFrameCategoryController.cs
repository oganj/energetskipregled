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
	public class TBEFrameCategoryController : BaseMvcController
	{
		private readonly ITBEFrameCategoryService _TBEFrameCategoryService;
		private readonly IMapperProvider _mapper;

		public TBEFrameCategoryController(ITBEFrameCategoryService TBEFrameCategoryService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (TBEFrameCategoryService == null) throw new ArgumentNullException("TBEFrameCategoryService");
			_TBEFrameCategoryService = TBEFrameCategoryService;
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
			List<TBEFrameCategory> result = _TBEFrameCategoryService.GetAll();
			var mapper = _mapper.GetMapper<TBEFrameCategory, TBEFrameCategoryDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}
	}
}
