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

namespace EnergetskiPregled.Controllers
{
	[Authorize]
	public class NonTrasparentBuildingElemetController : BaseMvcController
	{
		private readonly INonTrasparentBuildingElemetService _nonTrasparentBuildingElemetService;
		private readonly IMapperProvider _mapper;

		public NonTrasparentBuildingElemetController(INonTrasparentBuildingElemetService nonTrasparentBuildingElemetService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (nonTrasparentBuildingElemetService == null) throw new ArgumentNullException("nonTrasparentBuildingElemetService");
			_nonTrasparentBuildingElemetService = nonTrasparentBuildingElemetService;
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
			List<NonTrasparentBuildingElemet> result = new List<NonTrasparentBuildingElemet>();
			ApplicationUser user = await GetUserAsync();
			
			Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				result = _nonTrasparentBuildingElemetService.ListAll(project.Id);
			}

			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			return Json(result.Select(mapper.MapToDto).ToList());
		}

		[HttpGet]
		public async Task<ActionResult> List([FromQuery]BaseQuery request, [FromQuery]int projectId)
		{
			ApplicationUser user = await GetUserAsync();

			QueryResponse<NonTrasparentBuildingElemet> result = new QueryResponse<NonTrasparentBuildingElemet>();

			Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				result = _nonTrasparentBuildingElemetService.List(request, project.Id);
			}
			
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
			Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project == null)
			{
				return Json(null);
			}

			NonTrasparentBuildingElemet ev = _nonTrasparentBuildingElemetService.Get(id, project.Id);
			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();

			return Json(mapper.MapToDto(ev));
		}

		[HttpPost]
		public async Task<JsonResult> Create([FromBody]NonTrasparentBuildingElemetDto dto)
		{
			var mapperDto = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			NonTrasparentBuildingElemet entity = mapperDto.MapToEntity(dto, new NonTrasparentBuildingElemet());

			Project project = (await GetUserAsync()).Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				entity.ProjectId = project.Id;
				entity = await _nonTrasparentBuildingElemetService.CreateAndAssignToUser(entity);
			}
				
			return Json((mapperDto.MapToDto(entity)));
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int[] ids)
		{
			Project project = (await GetUserAsync()).Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				await _nonTrasparentBuildingElemetService.Archive(ids, project.Id);
			}
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
