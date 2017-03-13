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
	public class ProjectController : BaseMvcController
	{
		private readonly IProjectService _projectService;
		private readonly IMapperProvider _mapper;

		public ProjectController(IProjectService projectService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (projectService == null) throw new ArgumentNullException("ProjectService");
			_projectService = projectService;
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
			ApplicationUser user = await GetUserAsync();
			List<Project> result = _projectService.ListAll(user.Id);
			var mapper = _mapper.GetMapper<Project, ProjectDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}

		[HttpGet]
		public async Task<ActionResult> List([FromQuery]BaseQuery request)
		{
			ApplicationUser user = await GetUserAsync();
			QueryResponse<Project> result = _projectService.List(request, user.Id);
			var mapper = _mapper.GetMapper<Project, ProjectDto>();

			var response = new BaseQueryResponse<ProjectDto>
			{
				Total = result.Total,
				PageSize = result.PageSize,
				PageIndex = result.PageIndex,
				List = result.List.Select(mapper.MapToDto).ToList()
			};

			return Json(response);
		}

		[HttpGet]
		public async Task<ActionResult> Get([FromQuery]int id)
		{
			ApplicationUser user = await GetUserAsync();
			Project ev = _projectService.Get(id, user.Id);
			var mapper = _mapper.GetMapper<Project, ProjectDto>();

			return Json(mapper.MapToDto(ev));
		}

		[HttpPost]
		public async Task<JsonResult> Create([FromBody]ProjectDto dto)
		{
			var mapperDto = _mapper.GetMapper<Project, ProjectDto>();
			Project entity = mapperDto.MapToEntity(dto, new Project());
			entity = await _projectService.CreateAndAssignToUser(entity, (await GetUserAsync()).Id);
			return Json((mapperDto.MapToDto(entity)));
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int[] ids)
		{
			await _projectService.Archive(ids, (await GetUserAsync()).Id);
			return Json(new object());
		}

		[HttpPost]
		public async Task<JsonResult> Update([FromBody]ProjectDto dto)
		{
			var mapperDto = _mapper.GetMapper<Project, ProjectDto>();
			Project entity = mapperDto.MapToEntity(dto, _projectService.Get(dto.Id));
			entity = await _projectService.Update(entity);
			return Json((mapperDto.MapToDto(entity)));
		}
	}
}
