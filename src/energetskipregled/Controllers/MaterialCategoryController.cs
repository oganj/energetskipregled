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
	public class MaterialCategoryController : BaseMvcController
	{
		private readonly IMaterialCategoryService _MaterialCategoryService;
		private readonly IMapperProvider _mapper;

		public MaterialCategoryController(IMaterialCategoryService MaterialCategoryService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (MaterialCategoryService == null) throw new ArgumentNullException("MaterialCategoryService");
			_MaterialCategoryService = MaterialCategoryService;
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
			List<MaterialCategory> result = _MaterialCategoryService.ListAll();
			var mapper = _mapper.GetMapper<MaterialCategory, MaterialCategoryDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}

		//[HttpGet]
		//public async Task<ActionResult> List([FromQuery]BaseQuery request)
		//{
		//	ApplicationUser user = await GetUserAsync();
		//	QueryResponse<MaterialCategory> result = _MaterialCategoryService.List(request, user.Id);
		//	var mapper = _mapper.GetMapper<MaterialCategory, MaterialCategoryDto>();

		//	var response = new BaseQueryResponse<MaterialCategoryDto>
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
		//	MaterialCategory ev = _MaterialCategoryService.Get(id, user.Id);
		//	var mapper = _mapper.GetMapper<MaterialCategory, MaterialCategoryDto>();

		//	return Json(mapper.MapToDto(ev));
		//}

		//[HttpPost]
		//public async Task<JsonResult> Create([FromBody]MaterialCategoryDto dto)
		//{
		//	var mapperDto = _mapper.GetMapper<MaterialCategory, MaterialCategoryDto>();
		//	MaterialCategory entity = mapperDto.MapToEntity(dto, new MaterialCategory());
		//	entity = await _MaterialCategoryService.CreateAndAssignToUser(entity, (await GetUserAsync()).Id);
		//	return Json((mapperDto.MapToDto(entity)));
		//}

		//[HttpDelete]
		//public async Task<JsonResult> Delete(int[] ids)
		//{
		//	await _MaterialCategoryService.Archive(ids, (await GetUserAsync()).Id);
		//	return Json(new object());
		//}

		//[HttpPost]
		//public async Task<JsonResult> Update([FromBody]MaterialCategoryDto dto)
		//{
		//	var mapperDto = _mapper.GetMapper<MaterialCategory, MaterialCategoryDto>();
		//	MaterialCategory entity = mapperDto.MapToEntity(dto, _MaterialCategoryService.Get(dto.Id));
		//	entity = await _MaterialCategoryService.Update(entity);
		//	return Json((mapperDto.MapToDto(entity)));
		//}
	}
}
