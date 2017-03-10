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
	public class PlayerController : BaseMvcController
	{
		private readonly IPlayerService _playerService;
		private readonly IMapperProvider _mapper;

		public PlayerController(IPlayerService playerService, IMapperProvider mapper, IUserService userService)
			: base(userService)
		{
			if (playerService == null) throw new ArgumentNullException("playerService");
			_playerService = playerService;
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
			List<Player> result = _playerService.ListAll(user.Id);
			var mapper = _mapper.GetMapper<Player, PlayerDto>();

			return Json(result.Select(mapper.MapToDto).ToList());
		}

		[HttpGet]
		public async Task<ActionResult> List([FromQuery]BaseQuery request)
		{
			ApplicationUser user = await GetUserAsync();
			QueryResponse<Player> result = _playerService.List(request, user.Id);
			var mapper = _mapper.GetMapper<Player, PlayerDto>();

			var response = new BaseQueryResponse<PlayerDto>
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
			Player ev = _playerService.Get(id, user.Id);
			var mapper = _mapper.GetMapper<Player, PlayerDto>();

			return Json(mapper.MapToDto(ev));
		}

		[HttpPost]
		public async Task<JsonResult> Create([FromBody]PlayerDto dto)
		{
			var mapperDto = _mapper.GetMapper<Player, PlayerDto>();
			Player entity = mapperDto.MapToEntity(dto, new Player());
			entity = await _playerService.CreateAndAssignToUser(entity, (await GetUserAsync()).Id);
			return Json((mapperDto.MapToDto(entity)));
		}

		[HttpDelete]
		public async Task<JsonResult> Delete(int[] ids)
		{
			await _playerService.Archive(ids, (await GetUserAsync()).Id);
			return Json(new object());
		}

		[HttpPost]
		public async Task<JsonResult> Update([FromBody]PlayerDto dto)
		{
			var mapperDto = _mapper.GetMapper<Player, PlayerDto>();
			Player entity = mapperDto.MapToEntity(dto, _playerService.Get(dto.Id));
			entity = await _playerService.Update(entity);
			return Json((mapperDto.MapToDto(entity)));
		}
	}
}
