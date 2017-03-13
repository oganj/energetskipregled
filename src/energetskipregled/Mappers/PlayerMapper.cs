using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Common.Helpers;
using EnergetskiPregled.Contracts.Data;
using EnergetskiPregled.Contracts.Mappers;
using EnergetskiPregled.Models;

namespace EnergetskiPregled.Mappers
{
	public class PlayerMapper : ICustomMapper,
		IMapper<Player, PlayerDto>
	{
		public string[] TypeKeys
		{
			get
			{
				return new string[]
				{
					TypeHelper.GetTypesKey<Player, PlayerDto>()
				};
			}
		}

		public IMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
			where TEntity : class, new()
			where TDto : new()
		{
			return this as IMapper<TEntity, TDto>;
		}

		public PlayerDto MapToDto(Player entity)
		{
			var dto = new PlayerDto();
			dto.InjectFrom(entity);

			if (entity.CreatedBy != null)
			{
				dto.CreatedBy = new UserDto();
				dto.CreatedBy.InjectFrom(entity.CreatedBy);
			}

			if (entity.LastModifiedBy != null)
			{
				dto.LastModifiedBy = new UserDto();
				dto.LastModifiedBy.InjectFrom(entity.LastModifiedBy);
			}

			if (entity.User != null)
			{
				dto.User = new UserDto();
				dto.User.InjectFrom(entity.User);
			}

			return dto;
		}

		public Player MapToEntity(PlayerDto dto, Player e)
		{
			e.InjectFrom(dto);

			return e;
		}
	}
}
