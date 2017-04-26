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
		public async Task<ActionResult> ListAll()
		{
			List<NonTrasparentBuildingElemet> result = new List<NonTrasparentBuildingElemet>();
			ApplicationUser user = await GetUserAsync();

			Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				result = _nonTrasparentBuildingElemetService.ListAll(project.Id);
			}
			if (result == null || result.Count == 0 )
			{
				return Json(null);
			}
			var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			var mapperDtoNonTransparentBulidingElement = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			var mapperDtoMaterialThickness = _mapper.GetMapper<MaterialThickness, MaterialThicknessDto>();
			var mapperDtoMaterial = _mapper.GetMapper<Material, MaterialDto>();

			List<NonTrasparentBuildingElemetDto> responce = result.Select(mapperDtoNonTransparentBulidingElement.MapToDto).ToList();
			for (int i = 0; i < responce.Count; i++)
			{
				if (result[i].MaterialsUsed != null && result[i].MaterialsUsed.Count > 0)
				{
					responce[i].MaterialsUsed = new List<MaterialThicknessDto>();

					foreach (var item in result[i].MaterialsUsed)
					{
						MaterialThicknessDto tmp = mapperDtoMaterialThickness.MapToDto(item);
						tmp.Material = mapperDtoMaterial.MapToDto(item.Material);
						responce[i].MaterialsUsed.Add(tmp);
					}
				}
			}

			return Json(responce);
		}

		//[HttpGet]
		//public async Task<ActionResult> List([FromQuery]BaseQuery request, [FromQuery]int projectId)
		//{
		//	ApplicationUser user = await GetUserAsync();

		//	QueryResponse<NonTrasparentBuildingElemet> result = new QueryResponse<NonTrasparentBuildingElemet>();

		//	Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
		//	if (project != null)
		//	{
		//		result = _nonTrasparentBuildingElemetService.List(request, project.Id);
		//	}
			
		//	var mapper = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();

		//	var response = new BaseQueryResponse<NonTrasparentBuildingElemetDto>
		//	{
		//		Total = result.Total,
		//		PageSize = result.PageSize,
		//		PageIndex = result.PageIndex,
		//		List = result.List.Select(mapper.MapToDto).ToList()
		//	};

		//	return Json(response);
		//}

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
			var mapperDtoNonTransparentBulidingElement = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			var mapperDtoMaterialThickness = _mapper.GetMapper<MaterialThickness, MaterialThicknessDto>();

			NonTrasparentBuildingElemet entity = mapperDtoNonTransparentBulidingElement.MapToEntity(dto, new NonTrasparentBuildingElemet());
			entity.MaterialsUsed = entity.MaterialsUsed ?? new List<MaterialThickness>();
			foreach (var item in dto.MaterialsUsed)
			{
				entity.MaterialsUsed.Add(mapperDtoMaterialThickness.MapToEntity(item, new MaterialThickness()));
			}
			//entity.MaterialsUsed = dto.MaterialsUsed.Select(mapperDtoMaterialThickness.MapToEntity).ToList();// (go,new MaterialThickness()));

			Project project = (await GetUserAsync()).Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project != null)
			{
				entity.ProjectId = project.Id;
				if (entity.Id == new Int32())
				{//NEW
					entity = await _nonTrasparentBuildingElemetService.CreateAndAssignToUser(entity);
				}
				else
				{//UPDATE
					entity = await _nonTrasparentBuildingElemetService.Update(entity);
				}
				
			}
				
			return Json((mapperDtoNonTransparentBulidingElement.MapToDto(entity)));
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
			var mapperDtoNonTransparentBulidingElement = _mapper.GetMapper<NonTrasparentBuildingElemet, NonTrasparentBuildingElemetDto>();
			var mapperDtoMaterialThickness = _mapper.GetMapper<MaterialThickness, MaterialThicknessDto>();

			//Extract user and project
			ApplicationUser user = await GetUserAsync();
			Project project = user.Projects.FirstOrDefault();//TODO take passed project id when there is support for more then one project
			if (project == null)
			{
				return Json(null);
			}
			NonTrasparentBuildingElemet buildingELement = _nonTrasparentBuildingElemetService.Get(dto.Id, project.Id);


			NonTrasparentBuildingElemet entity = mapperDtoNonTransparentBulidingElement.MapToEntity(dto, buildingELement);
			entity.ProjectId = buildingELement.Project.Id;

			//DEAL with MAterial Thickness children
			foreach (var item in dto.MaterialsUsed?? new List<MaterialThicknessDto>())
			{
				if (item.Id == new int())
				{//new
					entity.MaterialsUsed.Add(mapperDtoMaterialThickness.MapToEntity(item, new MaterialThickness()));
				}
				else
				{//updated
					var tmp = entity.MaterialsUsed.FirstOrDefault(x => x.Id == item.Id);
					entity.MaterialsUsed.Remove(tmp);
					entity.MaterialsUsed.Add(mapperDtoMaterialThickness.MapToEntity(item, tmp));
				}
			}
			List<int> idsToBeRemoved = entity.MaterialsUsed.Select(go => go.Id).Except(dto.MaterialsUsed.Select(go1 => go1.Id)).ToList();
			foreach (var item in idsToBeRemoved)
			{//DELETED
				entity.MaterialsUsed.Remove(entity.MaterialsUsed.FirstOrDefault(go => go.Id == item));
			}

			entity = await _nonTrasparentBuildingElemetService.Update(entity);
			return Json((mapperDtoNonTransparentBulidingElement.MapToDto(entity)));
		}
	}
}
