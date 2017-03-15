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
	public class MaterialController : BaseMvcController
	{
		private readonly IMaterialService _MaterialService;
		private readonly IMapperProvider _mapper;

		public MaterialController(IMaterialService MaterialService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (MaterialService == null) throw new ArgumentNullException("MaterialService");
			_MaterialService = MaterialService;
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
			List<Material> result = _MaterialService.ListAll();
			var mapper = _mapper.GetMapper<Material, MaterialDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}

		//[HttpGet]
		//public async Task<ActionResult> List([FromQuery]BaseQuery request)
		//{
		//	ApplicationUser user = await GetUserAsync();
		//	QueryResponse<Material> result = _MaterialService.List(request, user.Id);
		//	var mapper = _mapper.GetMapper<Material, MaterialDto>();

		//	var response = new BaseQueryResponse<MaterialDto>
		//	{
		//		Total = result.Total,
		//		PageSize = result.PageSize,
		//		PageIndex = result.PageIndex,
		//		List = result.List.Select(mapper.MapToDto).ToList()
		//	};

		//	return Json(response);
		//}

		//[HttpGet]
		//public async Task<ActionResult> Get([FromQuery]int id)
		//{
		//	ApplicationUser user = await GetUserAsync();
		//	Material ev = _MaterialService.Get(id, user.Id);
		//	var mapper = _mapper.GetMapper<Material, MaterialDto>();

		//	return Json(mapper.MapToDto(ev));
		//}

		//[HttpPost]
		//public async Task<JsonResult> Create([FromBody]MaterialDto dto)
		//{
		//	var mapperDto = _mapper.GetMapper<Material, MaterialDto>();
		//	Material entity = mapperDto.MapToEntity(dto, new Material());
		//	entity = await _MaterialService.CreateAndAssignToUser(entity, (await GetUserAsync()).Id);
		//	return Json((mapperDto.MapToDto(entity)));
		//}

		//[HttpDelete]
		//public async Task<JsonResult> Delete(int[] ids)
		//{
		//	await _MaterialService.Archive(ids, (await GetUserAsync()).Id);
		//	return Json(new object());
		//}

		//[HttpPost]
		//public async Task<JsonResult> Update([FromBody]MaterialDto dto)
		//{
		//	var mapperDto = _mapper.GetMapper<Material, MaterialDto>();
		//	Material entity = mapperDto.MapToEntity(dto, _MaterialService.Get(dto.Id));
		//	entity = await _MaterialService.Update(entity);
		//	return Json((mapperDto.MapToDto(entity)));
		//}
	}
}
