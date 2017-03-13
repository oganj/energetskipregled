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
using Balansero.Contracts.Data;

namespace EnergetskiPregled.Controllers
{
	[Authorize]
	public class NonTrasparentBuildingElemetController : BaseMvcController
	{
		private readonly INonTrasparentBuildingElemetService _nonTrasparentBuildingElemetService;
		private readonly IMapperProvider _mapper;

		public NonTrasparentBuildingElemetController(INonTrasparentBuildingElemetService NonTrasparentBuildingElemetService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (NonTrasparentBuildingElemetService == null) throw new ArgumentNullException("NonTrasparentBuildingElemetService");
			_nonTrasparentBuildingElemetService = NonTrasparentBuildingElemetService;
			_mapper = mapper;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> ListAll([FromQuery]int projectId)
		{
			ApplicationUser user = await GetUserAsync();
			List<NonTrasparentBuildingElemet> result = _nonTrasparentBuildingElemetService.ListAll(projectId, user.Id);
			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}

		[HttpGet]
		public async Task<ActionResult> List([FromQuery]BaseQuery request, [FromQuery]int projectId)
		{
			ApplicationUser user = await GetUserAsync();
			QueryResponse<NonTrasparentBuildingElemet> result = _nonTrasparentBuildingElemetService.List(request, projectId, user.Id);
			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();

			var response = new BaseQueryResponse<NonTrasparentBuildingElemetDto>
			{
				Total = result.Total,
				PageSize = result.PageSize,
				PageIndex = result.PageIndex,
				List = result.List.Select(mapper.MapToDto).ToList()
			};

			return Json(response);
		}

		[HttpGet]
		public async Task<ActionResult> Get([FromQuery]int id, [FromQuery]int projectId)
		{
			ApplicationUser user = await GetUserAsync();
			NonTrasparentBuildingElemet ev = _nonTrasparentBuildingElemetService.Get(id, projectId, user.Id);
			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();

			return Json(mapper.MapToDto(ev));
		}

		[HttpPost]
		public async Task<JsonResult> Create([FromBody]NonTrasparentBuildingElemetDto dto)
		{
			var mapperDto = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			NonTrasparentBuildingElemet entity = mapperDto.MapToEntity(dto, new NonTrasparentBuildingElemet());
			entity = await _nonTrasparentBuildingElemetService.CreateAndAssignToUser(entity, (await GetUserAsync()).Id);
			return Json((mapperDto.MapToDto(entity)));
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int[] ids)
		{
			await _nonTrasparentBuildingElemetService.Archive(ids, (await GetUserAsync()).Id);
			return Json(new object());
		}

		[HttpPost]
		public async Task<JsonResult> Update([FromBody]NonTrasparentBuildingElemetDto dto)
		{
			var mapperDto = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			NonTrasparentBuildingElemet entity = mapperDto.MapToEntity(dto, _nonTrasparentBuildingElemetService.Get(dto.Id));
			entity = await _nonTrasparentBuildingElemetService.Update(entity);
			return Json((mapperDto.MapToDto(entity)));
		}
	}
}
